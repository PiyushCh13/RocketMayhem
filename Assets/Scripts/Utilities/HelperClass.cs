using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperClass : MonoBehaviour
{
    public static RectTransform TopWindow;
    public static RectTransform BottomWindow;
    public static float GetCameraDown(Camera camera) 
    {
        return (camera.transform.position.y - camera.orthographicSize) + BottomWindow.localScale.y - 0.1f;
    }

    public static float GetCamerUp(Camera camera)
    {
        return (camera.transform.position.y + camera.orthographicSize) - TopWindow.localScale.y + 0.1f;
    }

    public static float GetCameraRight(Camera camera)
    {
        return camera.transform.position.x + (camera.orthographicSize * camera.aspect);
    }

    public static float GetCameraLeft(Camera camera)
    {
        return camera.transform.position.x - (camera.orthographicSize * camera.aspect);
    }
}
