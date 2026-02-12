using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Grupp14
{
    public class DevDebugWindow : MonoBehaviour
    {
        private static bool showDebug = false;
        
        public void ToggleDebug(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                showDebug = !showDebug;
            }
        }

        private void OnGUI()
        {
            if (!showDebug) return;
            
            GUILayout.BeginArea(new Rect(10, 10, 300, 300));

            if (GUILayout.Button("Reload Scene"))
            {
                SceneManager.LoadScene(gameObject.scene.name);
            }
            
            GUILayout.EndArea();
        }
    }
}
