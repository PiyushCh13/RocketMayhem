using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;

    private void OnEnable()
    {
        scoreText.text = "SCORE: " + ((int)GameManager.Instance.Score).ToString();
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        GameManager.Instance.LoadScene(SceneList.GameScene.ToString());
        GameManager.Instance.currentGameStates = GameStates.isStarted;
        GameManager.Instance.hasInstantiatedPlayer = false;
    }

    public void Menu()
    {
        Time.timeScale = 1;
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        GameManager.Instance.LoadScene(SceneList.MainScreen.ToString());
        GameManager.Instance.currentGameStates = GameStates.inMenu;
    }
}
