using System;


/*
 * 窗口句柄的Windows实现
 */
namespace DesktopSprite
{
    class WindowsHandler : BaseHandler
    {
        public IntPtr hwnd;
        public WindowsHandler(IntPtr hwnd)
        {
            this.hwnd = hwnd;
        }
    }
}
