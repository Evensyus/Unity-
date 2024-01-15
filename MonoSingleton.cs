using UnityEngine;
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                // 查找存在的实例
                instance = (T)FindObjectOfType(typeof(T));

                // 如果不存在实例，则创建
                if (instance == null)
                {
                    // 需要创建一个游戏对象，再把这个单例组件挂载到游戏对象上
                    var singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T).ToString() + " (Singleton)";

                    // 让实例不在切换场景时销毁
                    DontDestroyOnLoad(singletonObject);
                }
            }

            return instance;
        }
    }
}

/// <summary>
/// 添加更多的其他语言
/// 形式类似为 Zh_CN 对应 "zh-CN";
/// </summary>
public enum SpeechSynthesisLanguage
{
    Zh_CN,
}

/// <summary>
/// 添加更多的其他声音
/// 形式类似为 Zh_CN_XiaochenNeural 对应 "zh-CN-XiaochenNeural";
/// </summary>
public enum SpeechSynthesisVoiceName
{
    Zh_CN_XiaochenNeural,
}