using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Runtime.InteropServices;


namespace DungeonCrawler.Rendering
{
    internal class GameRenderer
    {
        // Declatations
        static short windowXSize = 10;
        static short windowYSize = 3;


        // Imports the createfile method
        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern SafeFileHandle CreateFile(
                    string fileName,
                    [MarshalAs(UnmanagedType.U4)] uint fileAccess,
                    [MarshalAs(UnmanagedType.U4)] uint fileShare,
                    IntPtr securityAttributes,
                    [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
                    [MarshalAs(UnmanagedType.U4)] int flags,
                    IntPtr template);

        // Import the write console output method
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteConsoleOutputW(
          SafeFileHandle hConsoleOutput,
          CharInfo[] lpBuffer,
          Coord dwBufferSize,
          Coord dwBufferCoord,
          ref SmallRect lpWriteRegion);


        [STAThread] // Indicates I want to run this as singlethreaded?
        public static void WriteToBuffer(RenderTile[,] input)
        {
            // Sets window Sizes
            windowXSize = (short)input.GetLength(0);
            windowYSize = (short)input.GetLength(1);

            // converts to 1D array
            RenderTile[] usableInput = new RenderTile[input.GetLength(0) * input.GetLength(1)];

            for (int y = 0; y < input.GetLength(1); y++)
            {
                for (int x = 0; x < input.GetLength(0); x++)
                {
                    usableInput[ (y * input.GetLength(0)) + x ] = input[x, y];

                    if (usableInput[(y * input.GetLength(0)) + x] == null) 
                    {
                        usableInput[(y * input.GetLength(0)) + x] = new RenderTile('&');
                    }
                }
            }




            // Get the console buffer
            SafeFileHandle h = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);

            if (!h.IsInvalid)
            {
                // the buffer vars
                CharInfo[] buffer = new CharInfo[input.GetLength(0) * input.GetLength(1)];
                SmallRect rect = new SmallRect() { Left = 0, Top = 0, Right = windowXSize, Bottom = windowYSize };


                for (int i = 0; i < buffer.GetLength(0); ++i)
                {
                    buffer[i].Char.AsciiChar = (byte)usableInput[i].sprite;
                    buffer[i].Attributes = 4;
                }

                bool b = WriteConsoleOutputW
                (
                    h,
                    buffer,
                    new Coord() { X = windowXSize, Y = windowYSize },
                    new Coord() { X = 0, Y = 0 },
                    ref rect
                );

            }
        }
    }


    // ????? TODO Figure out WTF These structs are ??????
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode),]
    public struct Coord
    {
        public short X;
        public short Y;

        public Coord(short X, short Y)
        {
            this.X = X;
            this.Y = Y;
        }
    };

    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
    public struct CharUnion
    {
        [FieldOffset(0)] public ushort UnicodeChar;
        [FieldOffset(0)] public byte AsciiChar;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct CharInfo
    {
        [FieldOffset(0)] public CharUnion Char;
        [FieldOffset(2)] public short Attributes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SmallRect
    {
        public short Left;
        public short Top;
        public short Right;
        public short Bottom;
    }

}


