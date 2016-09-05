using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Runtime.InteropServices;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;

namespace sHook
{
        //全局钩子和私有钩子实现 

    public class PublicHook : HookBase
    {
        public PublicHook(HOOKPROC proc)
            : base(proc, HookType.WH_KEYBOARD_LL)
        { }

        public override int SetWindowsHookEx()
        {
            //if (m_hHook == 0)
            //    m_hHook = SetWindowsHookEx(this.m_type, this.m_proc, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), 0);
            //return m_hHook;

            if (m_hHook == 0)
            {
                Module m = Assembly.GetExecutingAssembly().GetModules()[0];
                IntPtr hInstance = Marshal.GetHINSTANCE(m);
                m_hHook = SetWindowsHookEx(this.m_type, MyKeyboardProc, hInstance, 0);

            }
            return m_hHook;

        }

        public int MyKeyboardProc(int nCode, int wParm, int lParam)
        {
            if ((char)wParm == (char)Keys.Escape)
            {
                Debug.Print("你已经按下了Esc!");
                return 1;
            }
            else
            {
                Debug.Print("你已经按下了按钮!");
                return CallNextHookEx(nCode, (IntPtr)wParm, (IntPtr)lParam);

            }
        }
    }



    public class PrivateHook : HookBase
    {
        public PrivateHook(HOOKPROC proc)
            : base(proc, HookType.WH_KEYBOARD)
        { }

        public override int SetWindowsHookEx()
        {
            if (m_hHook == 0)
                m_hHook = SetWindowsHookEx(this.m_type, this.m_proc, IntPtr.Zero, GetCurrentThreadId());
            return m_hHook;
        }
    }
}
