using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Diagnostics;
using Microsoft.Win32;



namespace sHook
{

    public abstract class HookBase
    {
        public delegate int HOOKPROC(int nCode, int wParam, int lParam);
        public enum HookType
        {
            WH_KEYBOARD = 2,//私有钩子
            WH_KEYBOARD_LL = 13//全局钩子
        }

        //设置钩子
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(HookType idHook, HOOKPROC lpfn, IntPtr hInstance, int threadId);
        //抽调钩子
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32")]
        public static extern int GetCurrentThreadId();

        /// <summary>
        /// 钩子处理委托
        /// </summary>
        public HOOKPROC m_proc;
        /// <summary>
        /// 钩子类型
        /// </summary>
        public HookType m_type;
        /// <summary>
        /// 钩子的句柄
        /// </summary>
        public int m_hHook = 0;

        public HookBase(HOOKPROC proc, HookType type)
        {
            this.m_proc = proc;
            this.m_type = type;
        }

        public abstract int SetWindowsHookEx();
        public virtual void UnhookWindowsHookEx()
        {
            bool retKeyboard = true;
            if (m_hHook != 0)
            {
                retKeyboard = UnhookWindowsHookEx(m_hHook);
                m_hHook = 0;
            }
            if (!retKeyboard)
            {
                throw new Exception("UnhookWindowsHookEx failed.");
            }
        }

        public virtual int CallNextHookEx(int nCode, IntPtr wParam, IntPtr lParam)
        {
            return CallNextHookEx(m_hHook, nCode, wParam, lParam);
        }
    }

}
