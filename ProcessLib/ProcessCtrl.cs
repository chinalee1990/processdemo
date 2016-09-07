using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.Windows.Forms;

namespace sProcessLib
{
    public class ProcessCtrl
    {
        Process m_process;
        bool m_bNewProcess = false;
        public void StartProcess(string strProcessPath,string strStartInfo)
        {
            m_process = Process.Start(strProcessPath, strStartInfo);
            m_bNewProcess = true;
        }

        public void CloseProcess()
        {
            try
            {
                m_process.CloseMainWindow();
                m_process.Close();
                m_process = null;
                m_bNewProcess = false;
            }
            catch
            {
                Debug.Print("失败：进程已关闭");
                return;
            }
            Debug.Print("关闭成功");
        }

        public void SetProcessParentWnd(Form parentform)
        {
            try
            {
                if (m_bNewProcess == true && m_process != null)
                {
                    int nResult = sWindowApi.WindowApi.SetWindowLong(m_process.MainWindowHandle, sWindowApi.WindowApi.GWL_HWNDPARENT, parentform.Handle);
                    m_bNewProcess = false;
                }
                Debug.Print("设置父窗口成功");
            }
            catch
            {
            }
        }

    }
}
