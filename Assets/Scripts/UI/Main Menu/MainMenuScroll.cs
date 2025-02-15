using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class MainMenuScroll : MonoBehaviour,IEndDragHandler
{
    [SerializeField] private int buttonIndex;

    [SerializeField] private Vector3 scrollOffset;
    private Vector3 targetPosition;

    [SerializeField] private float swipeSpeed;
    [SerializeField] Ease easeMode;

    private int childCount;


    private void Awake()
    {
        ScrollView.Initialise();
        buttonIndex = 0;
        childCount = ScrollView.scrollRect.content.transform.childCount - 1;
    }

    private void Update()
    {
        ScrollView.InteractableButton(buttonIndex);
    }

    public void Left()
    {
        if (buttonIndex > 0)
        {
            buttonIndex--;
            targetPosition.x += scrollOffset.x;
            SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
            ScrollView.ChangeButton(targetPosition, swipeSpeed , easeMode);
        }
    }

    public void Right()
    {
        if (buttonIndex < childCount)
        {
            buttonIndex++;
            targetPosition.x -= scrollOffset.x;
            SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
            ScrollView.ChangeButton(targetPosition, swipeSpeed, easeMode);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > ScrollView.dragTheresold)
        {
            if (eventData.pressPosition.x > eventData.position.x)
            {
                Right();
            }
            else
            {
                Left();
            }
        }

        else
        {
            ScrollView.ChangeButton(targetPosition, swipeSpeed, easeMode);
        }
    }
}
