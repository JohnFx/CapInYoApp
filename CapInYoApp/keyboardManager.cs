using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace BustACapInYourApp
{
    public static class keyboardManager
    {
        #region DLL Imports 

        /// <summary>
        /// This function retrieves the status of the specified virtual key.
        /// The status specifies whether the key is up, down.
        /// </summary>
        /// <param name="keyCode">Specifies a key code for the button to me checked</param>
        /// <returns>Return value will be 0 if off and 1 if on</returns>
        [DllImport("user32.dll")]
        internal static extern short GetKeyState(int keyCode);

        /// <summary>
        /// This function is useful to simulate Key presses to the window with focus.
        /// </summary>
        /// <param name="bVk">Specifies a virtual-key code. The code must be a value in the range 1 to 254.</param>
        /// <param name="bScan">Specifies a hardware scan code for the key.</param>
        /// <param name="dwFlags"> Specifies various aspects of function operation. This parameter can be one or more of the following values.
        ///                         <code>KEYEVENTF_EXTENDEDKEY</code> or <code>KEYEVENTF_KEYUP</code>
        ///                         If specified, the key is being released. If not specified, the key is being depressed.</param>
        /// <param name="dwExtraInfo">Specifies an additional value associated with the key stroke</param>
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        #endregion


        internal static bool isCapsLockOn() {
            return (GetKeyState((int)Keys.CapsLock)) != 0;
        }
        internal static void setCapsLock(bool capsLockOn) {
            if (isCapsLockOn() != capsLockOn) {
                PressKeyboardButton(Keys.CapsLock);
            }
        }

        /// <summary>
        /// Simulate the Key Press Event
        /// </summary>
        /// <param name="keyCode">The code of the Key to be simulated</param>
        private static void PressKeyboardButton(Keys keyCode) {
            const int KEYEVENTF_EXTENDEDKEY = 0x1;
            const int KEYEVENTF_KEYUP = 0x2;
            keybd_event((byte)keyCode, 0x45, KEYEVENTF_EXTENDEDKEY, 0);
            keybd_event((byte)keyCode, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
            //UpdateKeys();
        }
    }
}
