using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Tested
{
    public partial class CalledForm : Form
    {
        string[] strarrCmdLine;
        public CalledForm()
        {
            InitializeComponent();
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //int nScreenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            //int nScreenH = Screen.PrimaryScreen.WorkingArea.Height;
            //this.StartPosition = FormStartPosition.Manual;
            //this.Location = new Point(0, 0);
            //this.Width = nScreenWidth;
            //this.Height = nScreenH / 2;
            
        }

        public void SetCmdLine(string[] args)
        {
            strarrCmdLine = new string[args.Length];
            strarrCmdLine = args;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            foreach (string item in strarrCmdLine)
            {
                AddMsg(item);
            }
        }

        void AddMsg(string strMsg)
        {
            richTextBox1.Text += strMsg + "\r\n";

        }
        
        protected override void WndProc(ref Message m)
        {
            switch(m.Msg)
            {
                case 0x0400+100:
                    this.Activate();
                    if(this.WindowState== FormWindowState.Minimized)
                    {
                        this.WindowState = FormWindowState.Normal;
                    }
                    break;
            }
            base.WndProc(ref m);
        }


        private void Form1_Activated(object sender, EventArgs e)
        {
            //IntPtr hWnd = sWindowApi.WindowApi.FindWindow(null, "MainForm");
            //AddMsg(string.Format("MainForm = {0}", hWnd));
            //sWindowApi.WindowApi.PostMessage(hWnd, sWindowApi.WindowApi.m_nUserMsg, 0, 0);
        }



    }
}
