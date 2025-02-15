using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreMenu : MonoBehaviour
{
    public void OnClose() 
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        UIManager.Instance.OpenMenu(Menus.MainMenu);
        transform.DOScale(Vector3.zero, 1f);
    }
}
