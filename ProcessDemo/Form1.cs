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


namespace ProcessDemo
{
    
    public partial class Form1 : Form
    {
        Process m_process;
        Form2 m_form2; 
        public Form1()
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

        private void button1_Click(object sender, EventArgs e)
        {
            StartProcess();
        }

        void StartProcess()
        {
            string strPath = Environment.CurrentDirectory + "\\Test\\Tested.exe";
            m_process = Process.Start(strPath, "127.0.0.1");
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

        [DllImport("user32.dll")]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);


        [DllImport("user32.dll", EntryPoint = "ShowWindow", CharSet = CharSet.Auto)]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);
        //[DllImport("user32.dll")]
        //public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);
        public const int SW_SHOWMAXIMIZED = 3;
        //public const  int SW_MAXIMIZE = 3;
        public const int SW_SHOWNOACTIVATE = 4;
        //public const int SW_SHOW = 5;
        private const int WM_CLOSE = 0x0010;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int Width, int Height, int flags);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        private void Form1_Activated(object sender, EventArgs e)
        {

            textBox1.Text = "激活";
            //m_form2.Activate();
            try
            {
                SwitchToThisWindow(m_process.MainWindowHandle, true);
               // ShowWindow(m_process.MainWindowHandle, SW_SHOWNOACTIVATE);
                //SetWindowPos(m_process.MainWindowHandle, -1, 0, 0, 0, 0, 1 | 2);
                SetForegroundWindow(this.Handle);
                //this.TopMost = true;
                //Control control = Control.FromHandle(m_process.MainWindowHandle);
                //Form form = (Form)control;
                //form.Activate();
                Debug.Print("激活窗口成功");
            }
            catch
            {
                Debug.Print("激活窗口失败：进程无效");
            }
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

        private void Form1_Shown(object sender, EventArgs e)
        {
            Debug.Print("Form1_Shown");

        }

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            //this.contextMenuStrip1.Show(Cursor.Position);
        }


    }
}
