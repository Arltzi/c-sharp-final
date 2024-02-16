using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    internal class InputSystem
    {
        public static InputSystem instance = new InputSystem();

        public bool upper, downer, lefert, righert, shooter;

        // imports keyboard inputs
        [DllImport("user32.dll")]
        static extern bool GetAsyncKeyState(byte keyState);

        public void GetKeyboardInput()
        {
            byte[] keys = new byte[255];

            upper = GetAsyncKeyState(0x57);
            downer = GetAsyncKeyState(0x53);
            lefert = GetAsyncKeyState(0x41);
            righert = GetAsyncKeyState(0x44);
            /*
            // A
            if (GetAsyncKeyState(0x41) )
            {
                Application.inputMap = InputMap.LEFT;
            }

            // enter
            else if (GetAsyncKeyState(0x0D))
            {
                Application.inputMap = InputMap.SHOOT;
            }

            // s
            else if (GetAsyncKeyState(0x53))
            {
                Application.inputMap = InputMap.DOWN;

            }

            else if (GetAsyncKeyState(0x44))
            {
                Application.inputMap = InputMap.RIGHT;
            }

            // w
            else if (GetAsyncKeyState(0x57))
            {
                Application.inputMap = InputMap.UP;

            }

            else 
            {
                Application.inputMap = InputMap.NONE;
            }l
            */
        }

    }
}
