#define _CRT_SECURE_NO_WARNINGS

#include <stdlib.h>
#include <stdarg.h>
#include <stdio.h>
#include <inttypes.h>
#include <string.h>
#include <io.h>
#include <fcntl.h>

#pragma pack(1)

#define T_CALLOC(type) ((type*)calloc(1, sizeof(type)))
#define PTR(p, ofs) ((void*)((char*)(p) + (ofs)))
#define VAL(type, p, ofs) (*(type*)(PTR(p, ofs)))

void Crash(char* pMsg, ...) {
	printf("\n\n*** CRASH ***\n");
	va_list va;
	va_start(va, pMsg);
	vprintf(pMsg, va);
	va_end(va);
	printf("\n\n(Press enter to exit)\n");
	getchar();
	exit(1);
}

typedef struct tRVA tRVA;
struct tRVA {
	int baseAddress;
	int size;
	void* pData;
	tRVA* pNext;
};

typedef struct tMetaDataTable tMetaDataTable;
struct tMetaDataTable {
	void **ppData;
	int count;
};

#define TABLE_ID(token) ((token) >> 24)
#define TABLE_OFS(token) ((token) & 0x00ffffff)
#define MAKE_TOKEN(tableId, ofs) ((((int)(tableId)) << 24) | (((int)(ofs)) & 0x00ffffff))
#define GET_TABLE_ENTRY(type, pFile, token) (type*)(pFile->tables[TABLE_ID(token)].ppData[TABLE_OFS(token)])
#define GET_STRING(pFile, ofs) ((pFile)->pStrings + (ofs))

typedef struct tFile tFile;
struct tFile {
	tRVA* pRVAs;
	int entryPointToken;
	char *pStrings;
	char *pUserStrings;
	tMetaDataTable tables[64];
};

typedef struct tMD_Prefix tMD_Prefix;
struct tMD_Prefix {
	tFile *pFile;
};

#define TABLE_ID_METHODDEF 0x06
typedef struct tMD_MethodDef tMD_MethodDef;
struct tMD_MethodDef {
	tMD_Prefix prefix;
	int rva;
	short implFlags;
	short flags;
	short nameIndex; // Strings index
	short sigIndex; // Blob index
	short paramListIndex; // tMD_Param index
};

int md_tableSizes[64] = {
	10, 6, 14, 0, 6, 0, 14, 0, // 0x00 - 0x07
	6, 0, 6, 0, 6, 0, 0, 0,    // 0x08 - 0x0f
	0, 2, 0, 0, 0, 0, 0, 0,    // 0x10 - 0x17
	0, 0, 0, 0, 0, 0, 0, 0,    // 0x18 - 0x1f
	22, 0, 0, 20               // 0x20 - 0x23
};

// Start coding here...

void* RVA_FindData(tFile *pFile, int addr) {
	tRVA *pRVA = pFile->pRVAs;
	while (pRVA != NULL) {
		if (addr >= pRVA->baseAddress && addr < pRVA->baseAddress + pRVA->size) {
			return PTR(pRVA->pData, addr - pRVA->baseAddress);
		}
		pRVA = pRVA->pNext;
	}
	return NULL;
}

tFile* LoadFile(char *pFilename) {
	printf("Load file: '%s'\n", pFilename);

	// Load file
	int f = _open(pFilename, O_BINARY | O_RDONLY);
	int len = _lseek(f, 0, SEEK_END);
	_lseek(f, 0, SEEK_SET);
	void *pData = malloc(len);
	_read(f, pData, len);
	_close(f);

	tFile *pFile = T_CALLOC(tFile);

	if (VAL(char, pData, 0) != 'M' || VAL(char, pData, 1) != 'Z') Crash("Not an executable!");
	printf("Is an executable :)\n");
	void *pMSDOSHeader = pData;
	int lfanew = VAL(int, pMSDOSHeader, 0x3c);
	void *pPEHeader = PTR(pMSDOSHeader, lfanew + 4);
	if (VAL(short, pPEHeader, 0) != 0x14c) Crash("This is not a .NET executable!");
	printf("It is a .NET executable :)\n");
	void *pPEOptionalHeader = PTR(pPEHeader, 20);
	void *pSectionHeaders = PTR(pPEOptionalHeader, 224);
	int numSections = VAL(short, pPEHeader, 2);
	printf("Number of sections: %i\n", numSections);
	for (int i = 0; i < numSections; i++) {
		void *pSection = PTR(pSectionHeaders, i * 40);
		tRVA *pRVA = T_CALLOC(tRVA);
		pRVA->baseAddress = VAL(int, pSection, 12);
		pRVA->size = VAL(int, pSection, 8);
		pRVA->pData = calloc(1, pRVA->size);
		int rvaOfs = VAL(int, pSection, 20);
		memcpy(pRVA->pData, PTR(pData, rvaOfs), min(pRVA->size, VAL(int, pSection, 16)));
		pRVA->pNext = pFile->pRVAs;
		pFile->pRVAs = pRVA;
	}
	printf("Loaded sections :)\n");
	// Load CLI header
	void *pCLIHeader = RVA_FindData(pFile, VAL(int, pPEOptionalHeader, 208));
	printf("Runtime version: %i.%i\n", VAL(short, pCLIHeader, 4), VAL(short, pCLIHeader, 6));
	pFile->entryPointToken = VAL(int, pCLIHeader, 20);
	printf("entry-point token: 0x%08x\n", pFile->entryPointToken);
	void *pMetaData = RVA_FindData(pFile, VAL(int, pCLIHeader, 8));
	printf("CLI version: '%s'\n", (char*)PTR(pMetaData, 16));
	int versionLen = VAL(int, pMetaData, 12);
	int ofs = 16 + versionLen;
	int StreamCount = VAL(short, pMetaData, ofs + 2);
	printf("Metadata stream count: %i\n", StreamCount);
	ofs += 4;
	for (int i = 0; i < StreamCount; i++) {
		int streamOffset = VAL(int, pMetaData, ofs);
		int streamSize = VAL(int, pMetaData, ofs + 4);
		char *pStreamName = PTR(pMetaData, ofs + 8);
		void *pStream = PTR(pMetaData, streamOffset);
		ofs += 8 + ((strlen(pStreamName) + 4) & (~3));
		printf("Stream found: '%s'\n", pStreamName);
		if (strcmp(pStreamName, "#Strings") == 0) {
			pFile->pStrings = pStream;
		}
		if (strcmp(pStreamName, "#~") == 0) {
			// Load tables
			long long valid = VAL(long long, pStream, 8);
			int *pRowCounterPtr = PTR(pStream, 24);
			for (long long i = 0, j = 1; i < 64; i++, j <<= 1) {
				if (valid & j) {
					pFile->tables[i].count = *pRowCounterPtr;
					pRowCounterPtr += 1;
				}
			}
			// Load data
			void *pTableData = pRowCounterPtr;
			printf("Loaded tables:\n");
			for (int i = 0; i < 64; i++) {
				int count = pFile->tables[i].count;
				if (count != 0) {
					int tableSize = md_tableSizes[i];
					void **ppData = pFile->tables[i].ppData = malloc(sizeof(void*) * (count + 1));
					ppData[0] = NULL;
					for (int j = 0; j < count; j++) {
						tMD_Prefix *pPrefix = malloc(sizeof(tMD_Prefix) + tableSize);
						pPrefix->pFile = pFile;
						memcpy(pPrefix + 1, pTableData, tableSize);
						ppData[j + 1] = pPrefix;
						pTableData = PTR(pTableData, tableSize);
					}
					printf("  Table 0x%02x: %i entries\n", i, count);
				}
			}
		}
	}
	return pFile;
}

tMD_MethodDef* GetMethodByToken(tFile *pFile, int methodToken) {
	switch (TABLE_ID(methodToken)) {
	case TABLE_ID_METHODDEF:
		return GET_TABLE_ENTRY(tMD_MethodDef, pFile, methodToken);
	}
	Crash("!!!!");
	return NULL;
}

typedef struct tMethodState tMethodState;
struct tMethodState {
	tFile *pFile;
	tMD_MethodDef *pMethodDef;
	void *pIL;
	int ip;
	char evalStack[32];
	int esp;
};

tMethodState* CreateMethodState(tMD_MethodDef *pMethodDef) {
	tMethodState *pMethodState = T_CALLOC(tMethodState);
	pMethodState->pFile = pMethodDef->prefix.pFile;
	pMethodState->pMethodDef = pMethodDef;
	void *pILHeader = RVA_FindData(pMethodDef->prefix.pFile, pMethodDef->rva);
	if ((VAL(char, pILHeader, 0) & 0x3) != 0x2) Crash("Can only understand Tiny IL Headers");
	pMethodState->pIL = PTR(pILHeader, 1);
	return pMethodState;
}

void Execute(tMethodState *pMethodState) {
	tFile *pFile = pMethodState->pFile;
	printf("\nExecuting method: '%s'\n", GET_STRING(pFile, pMethodState->pMethodDef->nameIndex));
	void *pIL = pMethodState->pIL;
	void *pEvalStack = pMethodState->evalStack;
	for (;;) {
		unsigned char opcode = VAL(unsigned char, pIL, pMethodState->ip);
		pMethodState->ip += 1;
		printf("Executing opcode: 0x%02x\n", opcode);
		switch (opcode) {
		case 0x1f: // LDC.I4.S
		{
			// Load int32 from short-form (signed byte).
			int value = VAL(char, pIL, pMethodState->ip);
			pMethodState->ip += 1;
			// Store in top of evaluation stack.
			VAL(int, pEvalStack, pMethodState->esp) = value;
			pMethodState->esp += 4;
			break;
		}
		case 0x28: // CALL
		{
			// Load method-def|ref token from IL.
			// Only method-defs are handle currently. I.e. the method being called must in this assembly.
			int callToken = VAL(int, pIL, pMethodState->ip);
			pMethodState->ip += 4;
			// Load the method-definition from the metadata, and create a method state.
			tMD_MethodDef *pCallMethodDef = GetMethodByToken(pFile, callToken);
			tMethodState *pCallMethodState = CreateMethodState(pCallMethodDef);
			// Execute the method.
			Execute(pCallMethodState);
			// If it has a return-value, copy it to to evaluation-stack of this method.
			memcpy(PTR(pEvalStack, pMethodState->esp), pCallMethodState->evalStack, pCallMethodState->esp);
			pMethodState->esp += pCallMethodState->esp;
			break;
		}
		case 0x2a: // RET
			printf("Executing method return from: '%s' \n\n", GET_STRING(pFile, pMethodState->pMethodDef->nameIndex));
			return;
		case 0x58: // ADD
		{
			// Assume we're adding int32 values.
			// Read the top two int32 values from the evaluation stack, add them, then push back on to the evaluation stack.
			int value2 = VAL(int, pEvalStack, pMethodState->esp - 4);
			int value1 = VAL(int, pEvalStack, pMethodState->esp - 8);
			VAL(int, pEvalStack, pMethodState->esp - 8) = value1 + value2;
			pMethodState->esp -= 4;
			break;
		}
		default:
			Crash("Cannot (yet) execute opcode: 0x%02x\n", opcode);
		}
	}
}

int main(int argc, char** argp) {
	tFile *pFile = LoadFile(argp[1]);
	// Load entry-point method
	tMD_MethodDef *pEntryPointMethodDef = GetMethodByToken(pFile, pFile->entryPointToken);
	printf("Entry-point method name: '%s'\n", GET_STRING(pFile, pEntryPointMethodDef->nameIndex));
	// Create entry-point method state
	tMethodState *pMethodState = CreateMethodState(pEntryPointMethodDef);
	// Execute entry-point method
	Execute(pMethodState);

	int exitCode;
	if (pMethodState->esp == 4)
	{
		// If an int32 value is left at position 0 in the evaluation stack, then this is the exit-code.
		exitCode = VAL(int, pMethodState->evalStack, 0);
		printf("Execution completed successfully. Exit-code: %i", exitCode);
	}
	else
	{
		exitCode = 0;
		printf("Execution completed successfully. No exit-code");
	}

	printf("\n\n(Press enter to exit)\n");
	getchar();

	return exitCode;
}
