.include <N750.prog.mk>

PROG=	libIGraph.so.0.1
#PROG=	Test

SRCS=	libIGraph.c
SRCS+=	tinyjpeg.c
SRCS+=	jidctflt.c
SRCS+=	Bitmap.c
SRCS+=	BitmapLoader.c
SRCS+=	Brush.c
SRCS+=	DrawEllipses.c
SRCS+=	DrawLines.c
SRCS+=	DrawRectangles.c
SRCS+=	Exports.c
SRCS+=	Font.c
SRCS+=	FontFamily.c
SRCS+=	GetScreen.c
SRCS+=	Graphics.c
SRCS+=	Image.c
SRCS+=	Pen.c
SRCS+=	Pixels.c
SRCS+=	Region.c
SRCS+=	StringFormat.c
SRCS+=	Text.c

CFLAGS= -Os -I. -I./include -I./include/freetype -Wall -Werror
#-mips2 -mcpu=r3900
#CFLAGS= -O0 -g -I. -I./include -I./include/freetype -Wall -Werror

LDFLAGS+=--no-undefined -shared
LDADD+=./lib/libfreetype.so.9
LDADD+=-s

.include <bsd.prog.mk>

