/*
 * 继承unitychain！模型里的随机风场类，重写OnGUI函数，从而去掉屏幕上的风场开关
 * 后面可以把风场开关放到菜单中
 */
namespace FromUnityChain
{
    public class RandomWind : UnityChan.RandomWind
    {
        // Start is called before the first frame update
        void OnGUI()
        {
            isWindActive =true;
        }
    }
}

