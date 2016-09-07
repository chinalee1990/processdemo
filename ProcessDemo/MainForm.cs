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
        Form2 m_form2;

        private sHook.HookBase m_hook = null;
        sProcessLib.ProcessCtrl m_process = new sProcessLib.ProcessCtrl();
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
            string strPath = Environment.CurrentDirectory + "\\Test\\Tested.exe";
            string strInfo = string.Format("127.0.0.1 {0}", this.Height);
            m_process.StartProcess(strPath, strInfo);
            m_process.SetProcessParentWnd(this);
        }

        /// <summary>
        /// 结束进程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            m_process.CloseProcess();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_process.CloseProcess();
            CloseHook();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            Debug.Print("\r\nForm1_Activated");
            textBox1.Text = "激活";
        }

        void ActivateThis()
        {
            Debug.Print("激活当前窗口成功");
            sWindowApi.WindowApi.SetForegroundWindow(this.Handle);
            //this.Activate();
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            textBox1.Text = "未激活";
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

    }
}
