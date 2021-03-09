using UnityEngine;

namespace DesignPatterns
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T _instance;

        public static T Instance
        {
            get { return _instance; }
        }

        public static bool IsInitialized
        {
            get { return _instance != null; }
        }

        protected virtual void Awake()
        {
            if (_instance != null)
            {
                print("[Singleton] trying to instantiate a second instance of singleton class");
                return;
            }

            _instance = (T) this;
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
                _instance = null;
        }
    }
}
