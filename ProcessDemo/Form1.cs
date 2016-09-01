using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace ProcessDemo
{
    
    public partial class Form1 : Form
    {
        Process m_process;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartProcess();
        }

        void StartProcess()
        {
            string strPath = Environment.CurrentDirectory + "\\Test\\Tested.exe";
            m_process = Process.Start(strPath, "我是天才127.0.0.1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CloseProcess();
        }

        void CloseProcess()
        {
            try
            {
                m_process.CloseMainWindow();
                m_process.Close();
            }
            catch
            {
                Debug.Print("失败：进程已关闭");
                return;
            }
            Debug.Print("关闭成功");

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseProcess();
        }
    }
}
