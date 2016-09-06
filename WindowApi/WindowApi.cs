using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;

namespace sWindowApi
{
    public static class WindowApi
    {
        public const int m_nUserMsg = 0x0400+100;
        public const int m_nActiveMsg = 0x0006;
        public const int m_nMOUSEACTIVATE = 0x0021;
        [DllImport("user32.dll", EntryPoint = "SwitchToThisWindow")]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        [DllImport("user32.dll", EntryPoint = "PostMessage")]
        public static extern int PostMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.dll", EntryPoint = "SetActiveWindow")]
        public static extern IntPtr SetActiveWindow(IntPtr hwnd);


        [DllImport("User32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndlnsertAfter, int X, int Y, int cx, int cy, int Flags);

        //hWndInsertAfter 参数可选值:
        public const int m_HWND_TOP = 0;            //{在前面}
        public const  int m_HWND_BOTTOM = 1;         //{在后面}
        public const int m_HWND_TOPMOST = -1;      //{在前面, 位于任何顶部窗口的前面}
        public const int m_HWND_NOTOPMOST = -2;     //{在前面, 位于其他顶部窗口的后面}

        //uFlags 参数可选值:
        public const int m_SWP_NOSIZE = 1;          //{忽略 cx、cy, 保持大小}
        public const int m_SWP_NOMOVE = 2;          //{忽略 X、Y, 不改变位置}
        public const int m_SWP_NOZORDER = 4;        // {忽略 hWndInsertAfter, 保持 Z 顺序}
        public const int m_SWP_NOREDRAW = 8;        //{不重绘}
        public const int SWP_NOACTIVATE = 0x0010;
    }
}
