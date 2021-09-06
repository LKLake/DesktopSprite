using System.Collections;
using UnityEngine;


/*控制精灵移动与缩放
 * 左shift + 鼠标左键拖动：移动精灵
 * 左shift + 鼠标滚轮滑动：缩放精灵
 * 本来想用左ctrl，但ctrl加滚轮可能会缩放桌面
 */
namespace DesktopSprite
{
    public class SpriteControl : MonoBehaviour
    {
        private void OnMouseDown()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                StartCoroutine(OnMousedownCoroutine());
            }
        }
        private void Update()
        {
            //鼠标滚轮的效果(拷贝过来的)
            //Camera.main.fieldOfView 摄像机的视野
            //Camera.main.orthographicSize 摄像机的正交投影
            //Zoom out
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetAxis("Mouse ScrollWheel") < 0)
                {
                    if (Camera.main.fieldOfView <= 100)
                        Camera.main.fieldOfView += 2;
                    if (Camera.main.orthographicSize <= 20)
                        Camera.main.orthographicSize += 0.5F;
                }
                //Zoom in
                if (Input.GetAxis("Mouse ScrollWheel") > 0)
                {
                    if (Camera.main.fieldOfView > 2)
                        Camera.main.fieldOfView -= 2;
                    if (Camera.main.orthographicSize >= 1)
                        Camera.main.orthographicSize -= 0.5F;
                }
            }
        }

        IEnumerator OnMousedownCoroutine()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //创建一个平面
            Plane plane = new Plane(Vector3.forward, transform.position);
            //获取射线与平面交叉的点。
            plane.Raycast(ray, out float rayDistance);
            Vector3 target = ray.GetPoint(rayDistance);
            //人物中心与鼠标点击点的偏移量
            Vector3 offset = transform.position - target;
            while (Input.GetMouseButton(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //获取射线与平面交叉的点。
                plane.Raycast(ray, out rayDistance);
                target = ray.GetPoint(rayDistance);
                //鼠标拖动目标点加上偏移量，得到人物中心目标点
                Rigidbody rigidbody = GetComponent<Rigidbody>();
                //transform.position = target + offset ;
                rigidbody.MovePosition(target + offset);
                yield return new WaitForFixedUpdate();
            }
        }
    }
}

