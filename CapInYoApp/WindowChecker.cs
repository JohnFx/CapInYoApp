using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace BustACapInYourApp.windowInspector
{
    public class windowInfo {

        #region constructor and member properties
        public windowInfo(string appTitleToWatch) {
            checkFrequencyMS = 1000;
            appTitle = appTitleToWatch;
        }

        public volatile bool iSetCapsOn = false;
        public string appTitle { get; set; }
        public bool monitoringEnabled { get; set; }
        public int checkFrequencyMS { get; set; }
        #endregion

        #region DLL Imports

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        #endregion

        #region Delegates and callbacks definitions
        public delegate bool appFocusEventHandler();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newStatus"></param>
        /// <returns>boolean indicating whether the caps lock setting acutally channged</returns>
        public delegate void statusChangedHandler(string newStatus);

        bool windowIsActive() {
            if (!iSetCapsOn) {
                iSetCapsOn = true;
                keyboardManager.setCapsLock(true);
                return true;
            }
            else {
                return false;
            }
        }

        bool windowIsInActive() {
            if (iSetCapsOn) { //only turn off caps if we set it.
                keyboardManager.setCapsLock(false);
                iSetCapsOn = false;
                return true;
            }
            else {
                return false;
            }
        }
        #endregion

        #region Public interface
        public void startMonitoring(statusChangedHandler statusChangeHandler) {
            string activeWindowFileName;

            appFocusEventHandler AppHasFocusEventHandler = new windowInspector.windowInfo.appFocusEventHandler(windowIsActive);
            appFocusEventHandler AppLostFocusEventHandler = new windowInspector.windowInfo.appFocusEventHandler(windowIsInActive);
            monitoringEnabled = true;

            while (monitoringEnabled) {

                Thread.Sleep(checkFrequencyMS);
                IntPtr hWnd = GetForegroundWindow();
                uint procId = 0;
                Process proc = null;

                try { 
                    GetWindowThreadProcessId(hWnd, out procId);
                     proc = Process.GetProcessById((int)procId);
                }
                catch (System.ComponentModel.Win32Exception) {
                    //probably an access permission on thread. 
                }

                if (proc != null) {
                    try {
                        activeWindowFileName = proc.MainModule.FileName;                    
                    }
                    catch (System.ComponentModel.Win32Exception) {
                        activeWindowFileName = proc.MainWindowTitle;
                    }

                    if (activeWindowFileName.ToLower().Contains(appTitle.ToLower())) {
                        if (AppHasFocusEventHandler())  statusChangeHandler("Caps On"); 
                    }
                    else {
                        if (AppLostFocusEventHandler()) statusChangeHandler("Caps Off");
                    }     
                }
            }
        }

        public void stopMonitoring() {
            monitoringEnabled = false;
        }
        #endregion

    }
}
