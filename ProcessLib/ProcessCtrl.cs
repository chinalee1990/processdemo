using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

namespace sProcessLib
{
    public class ProcessCtrl
    {
        Process m_process;

        public Process Process
        {
            get { return m_process; }
            set { m_process = value; }
        }

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
                int nResult = 0;
                Debug.Print("m_bNewProcess = {0} m_process={1} ", m_bNewProcess, m_process);
                Debug.Print("m_process.MainWindowHandle = {0}", m_process.MainWindowHandle);

                int nCount = 0;
                while ((int)m_process.MainWindowHandle == 0)
                {
                    Thread.Sleep(5);
                    nCount++;
                    if (nCount >= 200)
                        break;
                }
                Debug.Print("WaitCount {0} ms m_process.MainWindowHandle = {1}", nCount*5,m_process.MainWindowHandle);


                int nSetCount = 0;
                if (m_bNewProcess == true && m_process != null && (int)m_process.MainWindowHandle>0)
                {
                    while (nResult==0)
                    {

                        nResult = sWindowApi.WindowApi.SetWindowLong(m_process.MainWindowHandle, sWindowApi.WindowApi.GWL_HWNDPARENT, parentform.Handle);

                        if (nResult>0)
                        {
                            Debug.Print("设置父窗口成功");
                            m_bNewProcess = false;
                        }
                        else
                        {
                            int nError = sWindowApi.WindowApi.GetLastError();
                            Debug.Print("设置父窗口失败 nError = {0}", nError);
                        }

                        nSetCount++;
                        if (nSetCount >= 5)
                            break;
                    }

                }

                Debug.Print("nResult = {0}", nResult);
            }
            catch
            {
                Debug.Print("SetProcessParentWnd 失败");

            }
        }


    }
}
