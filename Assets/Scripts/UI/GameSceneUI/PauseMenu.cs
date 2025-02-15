using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void Restart() 
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        GameManager.Instance.hasInstantiatedPlayer = false;
        GameManager.Instance.LoadScene(SceneList.GameScene.ToString());
    }

    public void Menu() 
    {
        Time.timeScale = 1;
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        GameManager.Instance.LoadScene(SceneList.MainScreen.ToString());
        GameManager.Instance.currentGameStates = GameStates.inMenu;
    }

    public void Resume() 
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        UIManager.Instance.StartCountdownCoroutine();
    }
}
