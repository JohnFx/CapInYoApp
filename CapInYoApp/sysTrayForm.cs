using System;
using System.Windows.Forms;
using BustACapInYourApp.windowInspector;
using System.Reflection;
using System.Threading;

namespace BustACapInYourApp
{
    public partial class sysTrayForm : Form
    {
        #region Constructor and Form Events 
        
        windowInfo windowWatcher = new windowInfo("Firefox");

        public sysTrayForm(string appToWatch)
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(appToWatch)) {
                windowWatcher.appTitle = appToWatch;
            }
        }

        #endregion

        #region system tray menu handlers
        private void sysTrayForm_Load(object sender, EventArgs e)
        {            
            Visible = false; // Hide form window.
            ShowInTaskbar = false; // Remove from taskbar.      

            new Thread(() => {
                Thread.CurrentThread.IsBackground = true;
                windowWatcher.startMonitoring(capsStatusChanged_event);
            }).Start();
        }

        #endregion

        #region Window watcher events
        void capsStatusChanged_event(string newStatus) {
            trayIcon.BalloonTipText = newStatus;
            trayIcon.ShowBalloonTip(1000);
        }

        #endregion

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }
    }
}