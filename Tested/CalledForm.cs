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
