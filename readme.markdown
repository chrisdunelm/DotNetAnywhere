Dot Net Anywhere
================
Dot Net Anywhere is a interpreted .NET CIL runtime.

-------

# This project is inactive. No issues or PRs will be dealt with.

*However! The code is being used in [Blazor](https://github.com/SteveSanderson/Blazor) IL-in-a-browser project :)*

-------

The runtime itself is written in C and has been designed to be as small and portable as possible, allowing .NET software to be used on resource-constrained devices where it is not possible to run a full .NET runtime (e.g. [Mono][1]).

How To Build
------------
The simplest way to build Dot Net Anywhere is using Visual Studio 2011 on Windows. Open and build both the solutions:

* dna.sln
* Managed.sln

This will create a 'Build/Debug/' directory which contains:

* ***dna.exe***: The CIL interpreter runtime *(native executable)*
* ***libIGraph.dll***: Low-levl graphics handling *(native library)*
* ***FreeType.dll***: The [FreeType][2] font engine *(native library)*
* ***/Fonts*** directory: Font .ttf files
* ***corlib.dll***: Dot Net Anywhere implementation of mscorlib.dll *(CIL library)*
* ***System.dll***:  Dot Net Anywhere implementation of System.dll *(CIL library)*
* ***System.Core.dll***:  Dot Net Anywhere implementation of System.Core.dll *(CIL library)*
* ***System.Drawing.dll***:  Dot Net Anywhere implementation of System.Drawing.dll; requires libIGraph.dll *(CIL library)*
* ***CustomDevice.dll***: Defines a the user interface of a custom device *(CIL library)*
* ***Snake.exe***: Demonstration game of snake *(CIL executable)*

How to use
----------

When at a command prompt in the Build/Debug directory:

```
dna.exe [<options>] <CIL executable> [<Cil executable arguments>]
```

So, to run the included snake game:

```
dna.exe Snake.exe
```

The Dot Net Anywhere interpreter can show all two levels of verbosity. Using the -v option shows initial .NET module load data and garbage collection information. Using -vv also shows all methods that are being JITted.

Of course, the Snake.exe is a completely standard .NET executable file, so it can just be run using the normal Microsoft .NET runtime:

```
Snake.exe
```

This game was originally written to run on some custom hardware that did not have a standard keyboard, hence the controls are little odd:

* '6': Left
* '7': Down
* '8': Up
* '9': Right

Supported .NET runtime features
-------------------------------

The interpreter and corlib currently implement the following .NET 2.0 CLR and language features:

* Generics
* Garbage collection and finalization
* Weak references
* Full exception handling - try/catch/finally
* PInvoke; although it's not the most pleasant or fully-featured implementation possible, but it will work cross-platform without libffi
* Interfaces
* Delegates
* Events
* Nullable types
* Single-dimensional arrays
* Multi-threading; not using native threads, only actually runs in one native thread
* Very limited read-only reflection; typeof(), .GetType(), Type.Name, Type.Namespace, Type.IsEnum(), \<object\>.ToString() only

**Currently unsupported features**

* Attributes
* Most reflection
* Multi-dimensional arrays
* Unsafe code

Implementation
--------------

The Dot Net Anywhere interpreter JITs each method as required into an internal format, which is then interpreted using a [direct-threaded][3] interpreter. The JIT stage does a full stack-type analysis and explicitly stores type information within the internal format's opcodes, allowing considerably more efficient interpretation that if the CIL was directly interpreted.

Custom platforms
-------------------------

Dot Net Anywhere has been designed to be fairly simple to port to custom platforms.

dna.exe and libIGraph.dll will need to be built for the platform. These are both written in C, and should build with most C compilers without problems. Two non-standard features are used:

1. Zero-length arrays: *char c[0];*
2. Computed goto: void *\*ptr = ...; goto \*ptr;* (this is not supported using the Visual Studio C compiler, so an assembly replacement is provided)

The only customisation that will generally be required is in the UI/input subsystem: The CustomDevice.dll managed library and the libIGraph.dll native library.

The embedded device that has been used for development has a 320x240 4-bit grey-scale screen and a 12-key keypad. This is all handled within the CustomDevice and libIGraph libraries, and will need to be customised for a device configuration.

To access the screen of the device, the CustomDevice class contains a method GetScreen() that returns a Graphics object that is the screen. If the device has a screen that is not a simple 2-D array of pixels then you will need to implement this differently.


[1]: http://www.mono-project.com
[2]: http://freetype.org
[3]: http://en.wikipedia.org/wiki/Threaded_code
