using UnityEngine;

public class UIChecker : MonoBehaviour
{
    void Start()
    {
        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
        {
            Debug.Log($"Canvas enabled: {canvas.enabled}");
            Debug.Log($"Render mode: {canvas.renderMode}");
            Debug.Log($"Camera: {canvas.worldCamera}");
        }
    }
}