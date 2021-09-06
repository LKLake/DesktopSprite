using System;
using UnityEngine;


/*
 * 窗口透明
 * 初始化窗口，包括设置分辨率、设置为透明、将窗口保持在上层
 */
namespace DesktopSprite
{
    public class TransparentWindow : MonoBehaviour
    {
        void Start()
        {
            try{
#if !UNITY_EDITOR
                WindowService windowService = new WindowService();
                /*获取目标窗口的句柄*/
                BaseHandler handler = windowService.GetWindowHandler(Application.productName);
                /*设置窗口大小为屏幕分辨率*/
                int width; int height;
                windowService.GetResolutionFromWindow(handler, out width, out height);
                Screen.SetResolution(width, height, true);
                /*对窗口的一些初始化工作，Windows下为去除边框*/
                windowService.InitWindow(handler);
                /*设置窗口为透明*/
                windowService.SetWindowTransparent(handler);
                /*将窗口放到最上层*/
                windowService.SetWindowTop(handler);
                /*使焦点可以穿透该窗口*/
                windowService.DisableWindowFocus(handler);
#endif
            }catch(NotImplementedException e){
                Debug.Log(e);
            }

        }
    }
}

