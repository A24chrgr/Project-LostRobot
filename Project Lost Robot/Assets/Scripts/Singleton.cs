using UnityEngine;

namespace Grupp14
{
    public static class Singleton
    {
        /// <summary>
        /// Attempts to make instance a singleton object. Returns true if successful, otherwise return false.
        /// </summary>
        /// <param name="singleton">A reference to the singleton variable</param>
        /// <param name="instance">The object instance to make a singleton</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool TrySetSingleton<T>(ref T singleton, T instance) where T: MonoBehaviour
        {
            if (singleton == null)
            {
                singleton = instance;
                instance.transform.SetParent(null);
                Object.DontDestroyOnLoad(instance.gameObject);
                return true;
            }
            else
            {
                Object.Destroy(instance.gameObject);
                return false;
            }
        }

        /// <summary>
        /// Attempts to set the singleton reference to null. Returns true if successful, otherwise return false.
        /// </summary>
        /// <param name="singleton">A reference to the singleton variable</param>
        /// <param name="instance">The object instance to remove singleton status</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool TryUnsetSingleton<T>(ref T singleton, T instance) where T : MonoBehaviour
        {
            if (singleton == instance)
            {
                singleton = null;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}