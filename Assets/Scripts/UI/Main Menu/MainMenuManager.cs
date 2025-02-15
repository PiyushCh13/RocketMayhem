using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour 
{
    [SerializeField] float speed;
    [SerializeField] Ease ease;

    private void Awake()
    {
        UIManager.Instance.InitialiseMainMenu();
    }

    public void Play() 
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        GameManager.Instance.LoadScene(SceneList.ModeSelectionScene.ToString());
        GameManager.Instance.hasInstantiatedPlayer = false;
    }

    public void Settings() 
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        UIManager.Instance.OpenMenu(Menus.Settings);
        UIManager.Instance.AnimateMenu(Vector3.one , Menus.Settings , speed , ease);
    }

    public void HighScore()
    {
        UIManager.Instance.highScoreText.text = GameManager.Instance.HighScore.ToString();
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        UIManager.Instance.OpenMenu(Menus.HighScore);
        UIManager.Instance.AnimateMenu(Vector3.one, Menus.HighScore, speed, ease);
    }

    public void Quit()
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        Application.Quit();
    }
}
