using System;
using System.Collections;
using UnityEngine;

/*
 * 鼠标穿透
 * 当光标移动到角色身上，响应输入事件，移开时让鼠标穿透到下层窗口。
 */
namespace DesktopSprite
{
    public class CurserPenetration : MonoBehaviour
    {
        private WindowService windowService;


        private void Start()
        {
            try{
                windowService = new WindowService();
            }
            catch(NotImplementedException e){
                Debug.Log(e);
            }
            StartCoroutine(Task());
        }


        IEnumerator Task()
        {
            int layer_mask = LayerMask.GetMask("Player");
            BaseHandler handler = windowService.GetWindowHandler(Application.productName);
            Ray ray;
            bool crtFocused;
            while (true)
            {
                //通过射线投射，确定鼠标光标是否在人物上，设置isFocused全局变量
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                crtFocused = Physics.Raycast(ray, Mathf.Infinity, layer_mask);
                if (Globle.isFocused != crtFocused)
                {
#if !UNITY_EDITOR
                    if (crtFocused)
                    {
                        windowService.EnableWindowFocus(handler);
                    }
                    else
                    {
                        windowService.DisableWindowFocus(handler);

                    }
#endif
                    Globle.isFocused = crtFocused;
                }
                //等待0.5秒再判断鼠标是否在人物上
                yield return new WaitForSeconds(.5f);
            }
        }
    }
}
