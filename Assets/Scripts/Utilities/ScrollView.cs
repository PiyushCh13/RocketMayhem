using DG.Tweening;
using System;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public static class ScrollView
{
    public static RectTransform rectTransform;
    public static ScrollRect scrollRect;

    public static float dragTheresold;

    public static Button leftButton;
    public static Button rightButton;

    public static void Initialise() 
    {
        scrollRect = GameObject.Find("ScrollView").GetComponent<ScrollRect>();
        rectTransform = scrollRect.content.GetComponent<RectTransform>();

        leftButton = GameObject.Find("Left").GetComponent<Button>();
        rightButton = GameObject.Find("Right").GetComponent<Button>();

        dragTheresold = Screen.width / 15f;
    }

    public static void InteractableButton(int buttonIndex) 
    {
        leftButton.interactable = buttonIndex == 0 ? false : true;
        rightButton.interactable = buttonIndex >= scrollRect.content.transform.childCount - 1 ? false : true;
    }

    public static void ChangeButton(Vector3 target , float speed , Ease ease)
    {
        rectTransform.DOLocalMove(target, speed).SetEase(ease);
    }
}