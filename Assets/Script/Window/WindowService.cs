using System;


/*
 * 窗口服务模块
 * 用于对外提供屏蔽了操作系统差异的窗口操作接口
 */
namespace DesktopSprite
{
    public class WindowService 
    {
        private IWindowController windowController;
        // Start is called before the first frame update
        public WindowService()
        {
#if UNITY_STANDALONE_LINUX

            throw new NotImplementedException("linux接口未实现");

#elif UNITY_STANDALONE_WIN

            windowController = new WindowsImpl();
#else
            throw new NotImplementedException("操作系统接口未实现");
#endif
        }
        #region 对外提供的服务
        public void EnableWindowFocus(string windowName)
        {
            BaseHandler handler = windowController.GetWindowHandler(windowName);
            windowController.EnableWindowFocus(handler);
        }
        public void EnableWindowFocus(BaseHandler handler)
        {
            windowController.EnableWindowFocus(handler);
        }
        public void DisableWindowFocus(string windowName)
        {
            BaseHandler handler = windowController.GetWindowHandler(windowName);
            windowController.DisableWindowFocus(handler);
        }
        public void DisableWindowFocus(BaseHandler handler)
        {
            windowController.DisableWindowFocus(handler);
        }
        public BaseHandler GetWindowHandler (string windowName){
            return windowController.GetWindowHandler(windowName);
        }
        public void SetWindowTransparent(string windowName)
        {
            BaseHandler handler = windowController.GetWindowHandler(windowName);
            windowController.SetWindowTransparent(handler);
            
        }
        public void SetWindowTransparent(BaseHandler handler)
        {
            windowController.SetWindowTransparent(handler);

        }
        public void SetWindowTop(string windowName)
        {
            BaseHandler handler = windowController.GetWindowHandler(windowName);
            windowController.SetWindowTop(handler);
        }
        public void SetWindowTop(BaseHandler handler)
        {
            windowController.SetWindowTop(handler);
        }
        public void InitWindow(BaseHandler handler)
        {
            windowController.InitWindow(handler);
        }
        public void InitWindow(string windowName)
        {
            BaseHandler handler = windowController.GetWindowHandler(windowName);
            windowController.InitWindow(handler);
        }
        public void GetResolutionFromWindow(string windowName,out int width,out int height)
        {
            BaseHandler handler = windowController.GetWindowHandler(windowName);
            windowController.GetResolutionFromWindow(handler,out width,out height);
        }
        public void GetResolutionFromWindow(BaseHandler handler, out int width, out int height)
        {
            windowController.GetResolutionFromWindow(handler, out width, out height);
        }
        #endregion
    }
}

