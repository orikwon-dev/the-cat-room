using UnityEngine;

public class MonoSingletonBase<T> : MonoBehaviour where T : MonoBehaviour
{
    #region Singleton
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_appIsQuitting)
            {
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        GameObject go = new GameObject("(Singleton)GameManager");
                        _instance = go.AddComponent<T>();
                        DontDestroyOnLoad(go);
                    }
                }
                    
                return _instance;
            }
        }
    }

    private void OnApplicationQuit()
    {
        _appIsQuitting = true;
    }

    private static readonly object _lock = new object();
    private static bool _appIsQuitting = false;
    #endregion
}
