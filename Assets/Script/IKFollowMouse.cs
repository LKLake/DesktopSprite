using UnityEngine;


/*
 * 视角追随
 * 添加到角色上，用于角色视角追随光标
 */

namespace DesktopSprite
{
    public class IKFollowMouse : MonoBehaviour
    {
        private Animator anim;
        private float Weight = 1;
        public float bodyWeight = 0.04f;
        public float headWeight = 0.4f;
        public float eyesWeight = 100;
        public float clampWeight = 0.5f;
        private bool isActive = true;
        private void OnAnimatorIK(int layerIndex)
        {
            if (!isActive)
            {
                return;
            }
            //返回一条射线从主摄像机通过鼠标的位置。
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //创建一个平面
            Plane plane = new Plane(Vector3.forward, transform.position);
            //射线的距离
            if (plane.Raycast(ray, out float rayDistance))
            {
                //获取射线与平面交叉的点。
                Vector3 target = ray.GetPoint(rayDistance);
                //让角色注视当前点击的点
                anim.SetLookAtPosition(target);
                //设置角色的IK权重
                anim.SetLookAtWeight(Weight, bodyWeight, headWeight, eyesWeight, clampWeight);
            }
        }
        private void Start()
        {
            anim = GetComponent<Animator>();
            
        }
        // Update is called once per frame
        void Update()
        {
            /*通过判断是否获取焦点，决定是否进行视线跟随。
             * 通过对Weight参数插值，让视线跟随的开始和结束更加平滑*/
            if (!Globle.isFocused)
            {
                isActive = true;
                if (Weight >= 0.99)
                {
                    Weight = 1;
                }
                else
                {
                    Weight = Mathf.Lerp(Weight, 1, 0.05f);
                }
            }
            else
            {Debug.Log(Weight);
                Weight = Mathf.Lerp(Weight, 0, 0.05f);
                if(Weight<=0.01)
                {
                    Weight = 0;
                    isActive = false;
                }
            }
        }
    }
}

