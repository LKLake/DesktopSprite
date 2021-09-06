using UnityEngine;


/*控制精灵身体姿势切换
 * 左alt + 鼠标滚轮下滚 ：切换下一个姿势
 * 左alt + 鼠标滚轮上滚 ：切换上一个姿势
 * 注：使用unity chan！模型里FaceUpdate脚本的思想
 */

namespace FromUnityChain
{
	public class ActionUpdate : MonoBehaviour
	{
		Animator anim;
		private AnimatorStateInfo currentState;
		private AnimatorStateInfo previousState;


		void Start()
		{
			anim = GetComponent<Animator>();
			currentState = anim.GetCurrentAnimatorStateInfo(1);
			previousState = currentState;
			anim.SetLayerWeight(0, 1);
		}

		void Update()
		{
			if (Input.GetKey(KeyCode.LeftAlt))
			{
				if (Input.GetAxis("Mouse ScrollWheel") < 0)
				{
					anim.SetBool("Next", true);
				}
				if (Input.GetAxis("Mouse ScrollWheel") > 0)
				{

					anim.SetBool("Back", true);
				}
			}
			if (anim.GetBool("Next"))
			{
				currentState = anim.GetCurrentAnimatorStateInfo(0);
				if (previousState.fullPathHash != currentState.fullPathHash)
				{
					anim.SetBool("Next", false);
					previousState = currentState;
				}
			}

			if (anim.GetBool("Back"))
			{
				currentState = anim.GetCurrentAnimatorStateInfo(0);
				if (previousState.fullPathHash != currentState.fullPathHash)
				{
					anim.SetBool("Back", false);
					previousState = currentState;
				}
			}
		}
	}
}

