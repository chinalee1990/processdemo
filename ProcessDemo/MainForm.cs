using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;


using System.Runtime.InteropServices;

using sHook;


namespace ProcessDemo
{
    
    public partial class MainForm : Form
    {
        Process m_process;
        Form2 m_form2;

        private sHook.HookBase m_hook = null;

        public MainForm()
        {
            InitializeComponent();
            int nScreenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int nScreenH = Screen.PrimaryScreen.WorkingArea.Height;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, nScreenH / 2);
            this.Width = nScreenWidth;
            this.Height = nScreenH / 2;
            this.KeyPreview = true;
            //m_form2 = new Form2();
            //m_form2.Show();

        }


        /// <summary>
        /// 启动进程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            StartProcess();
        }

        void StartProcess()
        {
            string strPath = Environment.CurrentDirectory + "\\Test\\Tested.exe";
            m_process = Process.Start(strPath, "127.0.0.1");
        }

        /// <summary>
        /// 结束进程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            CloseHook();
        }



        private void Form1_Activated(object sender, EventArgs e)
        {
            Debug.Print("\r\nForm1_Activated");

            textBox1.Text = "激活";

            //if (m_bAcivateSub==false)
            //{
                ActivateSub();
            //}

            //if (m_bActivateThis==false)
            //{
                ActivateThis();
            //}   
        }

        bool m_bAcivateSub = false;
        bool m_bActivateThis = false;
        void ActivateSub()
        {
            try
            {
                //WindowApi.SetForegroundWindow(m_process.MainWindowHandle);
                Debug.Print("子进程窗口激活成功");
                sWindowApi.WindowApi.SendMessage(m_process.MainWindowHandle, sWindowApi.WindowApi.m_nUserMsg, 0, 0);
                //bFlag = false;
            }
            catch
            {
                Debug.Print("子进程窗口激活成功：进程无效");
            }
        }

        void ActivateThis()
        {
            //WindowApi.SwitchToThisWindow(this.Handle, true);
            //Debug.Print("激活当前窗口成功");
            this.Activate();
            m_bActivateThis = true;
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            textBox1.Text = "未激活";
            m_bActivateThis = false;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            //this.contextMenuStrip1.Show(Cursor.Position);
        }


        void OpenHook()
        {

            //hook = new sHook.PublicHook(MyKeyboardProc);

            m_hook = new sHook.PrivateHook(MyKeyboardProc);
            int nHook = m_hook.SetWindowsHookEx();
            if (nHook == 0)
            {
                MessageBox.Show("设置钩子失败!");
            }
        }

        void CloseHook()
        {
            try
            {
                m_hook.UnhookWindowsHookEx();
            }
            catch
            {
                Debug.Print("取消钩子失败");
            }
        }

        public int MyKeyboardProc(int nCode, int wParm, int lParam)
        {
            if ((char)wParm == (char)Keys.Escape)
            {
                MessageBox.Show("你已经按下了Esc!");
                this.Close();
                return 1;
            }
            else
            {
                MessageBox.Show("你已经按下了按钮!");
                return m_hook.CallNextHookEx(nCode, (IntPtr)wParm, (IntPtr)lParam);

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //OpenHook();
        }



        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0400 + 100:
                    //m_bAcivateSub = true;
                    //if(m_bActivateThis==false)
                    //{
                    //    ActivateThis();
                    //}


                    //Debug.Print("接收到子进程窗口激活消息");
                    break;
            }
            base.WndProc(ref m);
        }
    }
}
