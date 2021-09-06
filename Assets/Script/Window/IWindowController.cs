/*
 * 窗口controller的接口，不同操作系统各自实现
 */
namespace DesktopSprite
{
    public interface IWindowController
    {
        BaseHandler GetWindowHandler(string windowName);
        void EnableWindowFocus(BaseHandler handler);
        void DisableWindowFocus(BaseHandler handler);
        void SetWindowTop(BaseHandler handler);
        void SetWindowTransparent(BaseHandler handler);
        void InitWindow(BaseHandler handler);
        void GetResolutionFromWindow(BaseHandler handler, out int width, out int height);
    }
}

