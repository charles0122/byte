
using UnityEngine;
namespace ByteLoop.Tool
{
    public abstract class PersistentMonoSingleton<T> : MonoBehaviour where T : Component
    {
        protected static T mInstance;
        public static T Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = FindObjectOfType<T>();
                    if (mInstance == null)
                    {
                        var obj = new GameObject();
                        mInstance = obj.AddComponent<T>();
                    }
                }

                return mInstance;
            }
        }

        protected virtual void Awake()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            if (mInstance == null)
            {
                mInstance = this as T;
                DontDestroyOnLoad(transform.gameObject);
            }
            else
            {
  
                if (this != mInstance)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}