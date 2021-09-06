using System;
using System.Runtime.InteropServices;


/*
 * 窗口controller层的Windows操作系统实现
 * 仅供WindowService调用
 */
namespace DesktopSprite
{
    
    public class WindowsImpl: IWindowController
    {
        #region 引用WindowsAPI
        // Set properties of the window
        // See: https://msdn.microsoft.com/en-us/library/windows/desktop/ms633591%28v=vs.85%29.aspx
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
        [DllImport("User32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // Extend the window into the client area
        // See: https://msdn.microsoft.com/en-us/library/windows/desktop/aa969512%28v=vs.85%29.aspx 
        //将窗口扩展到客户区，客户区指窗口的边框标题等区域。
        [DllImport("Dwmapi.dll")]
        private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern bool GetMonitorInfo(IntPtr hmonitor, [In, Out] MONITORINFOEX info);

        [DllImport("user32.dll")]
        private static extern IntPtr MonitorFromWindow(IntPtr hwnd, MonitorFlag flag);
        #endregion


        #region WS参数
        private const uint WS_POPUP = 0x80000000;
        private const uint WS_VISIBLE = 0x10000000;
        private const uint WS_EX_TOPMOST = 0x00000008;
        private const uint WS_EX_LAYERED = 0x00080000;
        private const uint WS_EX_TRANSPARENT = 0x00000020;
        #endregion


        #region GWL参数
        private const int GWL_STYLE = -16;
        private const int GWL_EXSTYLE = -20;
        #endregion


        #region 数据结构
        private struct MARGINS
        {
            public int cxLeftWidth;
            public int cxRightWidth;
            public int cyTopHeight;
            public int cyBottomHeight;
        }
        private readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
        private class MONITORINFOEX
        {
            public int cbSize = Marshal.SizeOf(typeof(MONITORINFOEX));
            public RECT rcMonitor = new RECT();
            public RECT rcWork = new RECT();
            public int dwFlags = 0;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public char[] szDevice = new char[32];
        }
   

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        private enum MonitorFlag : uint
        {
            /// <summary>Returns NULL.</summary>
            MONITOR_DEFAULTTONULL = 0,
            /// <summary>Returns a handle to the primary display monitor.</summary>
            MONITOR_DEFAULTTOPRIMARY = 1,
            /// <summary>Returns a handle to the display monitor that is nearest to the window.</summary>
            MONITOR_DEFAULTTONEAREST = 2
        }

        #endregion


        #region 实现接口
        public BaseHandler GetWindowHandler(string windowName)
        {
            IntPtr hwnd;
            hwnd = FindWindow(null, windowName);
            if (hwnd == IntPtr.Zero)
            {
                //hwnd = GetForegroundWindow();
            }
            return new WindowsHandler(hwnd);
        }

        public void EnableWindowFocus(BaseHandler handler)
        {
            SetWindowLong(((WindowsHandler)handler).hwnd, GWL_EXSTYLE, WS_EX_LAYERED);
        }

        public void DisableWindowFocus(BaseHandler handler)
        {
            SetWindowLong(((WindowsHandler)handler).hwnd, GWL_EXSTYLE, WS_EX_TRANSPARENT | WS_EX_LAYERED);
        }

        public void SetWindowTop(BaseHandler handler)
        {
            SetWindowPos(((WindowsHandler)handler).hwnd, HWND_TOPMOST, 0, 0, 0, 0, 1 | 2);
        }

        public void SetWindowTransparent(BaseHandler handler)
        {
            /*在Windows的窗口管理器中，当一个窗口被设置如下属性，
             * 那么窗口中像素点rgba为(0,0,0,0,)的区域会被设置为透明
             * 因此，在unity中将背景色设为solid color，颜色为(0,0,0,0,)
             * 再执行此接口，就可以实现窗口透明，角色还在。
             */
            SetWindowLong(((WindowsHandler)handler).hwnd, GWL_STYLE, WS_POPUP | WS_VISIBLE);
        }

        public void InitWindow(BaseHandler handler)
        {
            var margins = new MARGINS() { cxLeftWidth = -1 };
            DwmExtendFrameIntoClientArea(((WindowsHandler)handler).hwnd, ref margins);
        }

        public void GetResolutionFromWindow(BaseHandler handler, out int width, out int height)
        {
            IntPtr monitor = MonitorFromWindow(((WindowsHandler)handler).hwnd, MonitorFlag.MONITOR_DEFAULTTOPRIMARY);
            MONITORINFOEX montorInfo = new MONITORINFOEX();
            GetMonitorInfo(monitor, montorInfo);
            width = montorInfo.rcMonitor.right - montorInfo.rcMonitor.left;
            height = montorInfo.rcMonitor.bottom - montorInfo.rcMonitor.top;
        }
        #endregion
    }
}

