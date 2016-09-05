using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Tested
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            foreach (var item in args)
            {
                
            }
            CalledForm mainform = new CalledForm();

            mainform.SetCmdLine(args);
            Application.Run(mainform);
        }
    }
}
