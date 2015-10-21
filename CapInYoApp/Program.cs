using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BustACapInYourApp.windowInspector;

namespace BustACapInYourApp
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string appTitle = string.Empty;
            if (args.Length > 0) appTitle = args[0];
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new sysTrayForm(appTitle));
        }
    }

    

}