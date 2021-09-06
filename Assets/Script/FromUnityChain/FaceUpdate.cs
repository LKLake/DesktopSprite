using UnityEngine;


/*控制精灵面部表情切换
 * 左alt + 鼠标左键单击 ：切换下一个表情
 * 左alt + 鼠标右键单击 ：切换上一个表情
 * 注：使用unity chan！模型里FaceUpdate脚本的思想
 */
namespace FromUnityChain
{
	public class FaceUpdate : MonoBehaviour
	{
		Animator anim;
		private AnimatorStateInfo currentState;
		private AnimatorStateInfo previousState;


		void Start()
		{
			anim = GetComponent<Animator>();
			currentState = anim.GetCurrentAnimatorStateInfo(1);
			previousState = currentState;
			anim.SetLayerWeight(1, 1);
		}

		void Update()
		{
			if (Input.GetKey(KeyCode.LeftAlt))
			{
				if (Input.GetMouseButton(0))
				{
					anim.SetBool("NextExpression", true);
				}
				if (Input.GetMouseButton(1))
				{

					anim.SetBool("BackExpression", true);
				}
			}
			if (anim.GetBool("NextExpression"))
			{
				currentState = anim.GetCurrentAnimatorStateInfo(1);
				if (previousState.fullPathHash != currentState.fullPathHash)
				{
					anim.SetBool("NextExpression", false);
					previousState = currentState;
				}
			}
			if (anim.GetBool("BackExpression"))
			{
				currentState = anim.GetCurrentAnimatorStateInfo(1);
				if (previousState.fullPathHash != currentState.fullPathHash)
				{
					anim.SetBool("BackExpression", false);
					previousState = currentState;
				}
			}
		}
	}
}

