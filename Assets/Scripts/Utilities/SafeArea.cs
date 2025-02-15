using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    public RectTransform rectTransform;
    public Rect safeArea;

    public Vector2 minAnchor;
    public Vector2 maxAnchor;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        safeArea = Screen.safeArea;

        minAnchor = safeArea.position;
        maxAnchor = safeArea.position + safeArea.size;

        minAnchor.x /= Screen.width;
        maxAnchor.x /= Screen.width;

        maxAnchor.y /= Screen.height;
        minAnchor.y /= Screen.height;

        rectTransform.anchorMin = minAnchor;
        rectTransform.anchorMax = maxAnchor;
    }
}
