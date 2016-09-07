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

        #region SetWindowPos
        [DllImport("User32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndlnsertAfter, int X, int Y, int cx, int cy, int Flags);
        /*
        * SetWindowPos Flags
        */
        public const short SWP_NOSIZE = 0x0001;
        public const short SWP_NOMOVE = 0x0002;
        public const short SWP_NOZORDER = 0x0004;
        public const short SWP_NOREDRAW = 0x0008;
        public const short SWP_NOACTIVATE = 0x0010;
        public const short SWP_FRAMECHANGED = 0x0020;  /* The frame changed: send WM_NCCALCSIZE */
        public const short SWP_SHOWWINDOW = 0x0040;
        public const short SWP_HIDEWINDOW = 0x0080;
        public const short SWP_NOCOPYBITS = 0x0100;
        public const short SWP_NOOWNERZORDER = 0x0200;  /* Don't do owner Z ordering */
        public const short SWP_NOSENDCHANGING = 0x0400;  /* Don't send WM_WINDOWPOSCHANGING */
        #endregion

        #region SetWindowLong  GetWindowLong
        [DllImport("User32.dll", EntryPoint = "SetWindowLong")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
        public const int  GWL_WNDPROC = -4;
        public const int  GWL_HINSTANCE = -6;
        public const int  GWL_HWNDPARENT = -8;
        public const int  GWL_STYLE = -16;
        public const int  GWL_EXSTYLE = -20;
        public const int  GWL_USERDATA = -21;
        public const int  GWL_ID = -12;
        /*
         * Window Styles
         */
         public const uint WS_OVERLAPPED    = 0x00000000;
         public const uint WS_POPUP = 0x80000000;
         public const uint WS_CHILD = 0x40000000;
         public const uint WS_MINIMIZE = 0x20000000;
         public const uint WS_VISIBLE = 0x10000000;
         public const uint WS_DISABLED = 0x08000000;
         public const uint WS_CLIPSIBLINGS = 0x04000000;
         public const uint WS_CLIPCHILDREN = 0x02000000;
         public const uint WS_MAXIMIZE = 0x01000000;
         public const uint WS_CAPTION = 0x00C00000;    /* WS_BORDER | WS_DLGFRAME  */
         public const uint WS_BORDER = 0x00800000;
         public const uint WS_DLGFRAME = 0x00400000;
         public const uint WS_VSCROLL = 0x00200000;
         public const uint WS_HSCROLL = 0x00100000;
         public const uint WS_SYSMENU = 0x00080000;
         public const uint WS_THICKFRAME = 0x00040000;
         public const uint WS_GROUP = 0x00020000;
         public const uint WS_TABSTOP = 0x00010000;
         public const uint WS_MINIMIZEBOX = 0x00020000;
         public const uint WS_MAXIMIZEBOX = 0x00010000;
         /*
         * Extended Window Styles
         */
         public const uint WS_EX_DLGMODALFRAME = 0x00000001;
         public const uint WS_EX_NOPARENTNOTIFY = 0x00000004;
         public const uint WS_EX_TOPMOST        = 0x00000008;
         public const uint WS_EX_ACCEPTFILES    = 0x00000010;
         public const uint WS_EX_TRANSPARENT    = 0x00000020;
         public const uint WS_EX_MDICHILD       = 0x00000040;
         public const uint WS_EX_TOOLWINDOW     = 0x00000080;
         public const uint WS_EX_WINDOWEDGE     = 0x00000100;
         public const uint WS_EX_CLIENTEDGE     = 0x00000200;
         public const uint WS_EX_CONTEXTHELP    =0x00000400;
         [DllImport("User32.dll", EntryPoint = "SetWindowLong")]
         public static extern uint GetWindowLong(IntPtr hWnd, int nIndex);
         #endregion

        [DllImport("Kernel32.dll", EntryPoint = "GetLastError")]
        public static extern int GetLastError();

        [DllImport("Kernel32.dll", EntryPoint = "SetLastError")]
        public static extern void SetLastError(int nErrorCode);

        [DllImport("User32.dll", EntryPoint = "MoveWindow")]
        public static extern uint MoveWindow(IntPtr hWnd,int X,int Y,int nW,int nH,uint bRepaint);

    }
}
