using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public enum Menus 
{
    Settings,
    HighScore,
    MainMenu,
    Paused,
    GameOver,
    InGame
}

public class UIManager : Singleton<UIManager>
{
    public GameObject canvas, settings, highScore, screen, pause, gameOver;
    public TMP_Text countdownText;
    public TMP_Text highScoreText;
    public Button pauseButton;

    public void InitialiseMainMenu()
    {
       canvas = GameObject.Find("Canvas");
       settings = canvas.transform.Find("Settings").gameObject;
       highScore = canvas.transform.Find("HighScore").gameObject;
       screen = canvas.transform.Find("Screen").gameObject;
       highScoreText = canvas.transform.Find("HighScore/HighScoreBG/MiddleArea/HighScoreText").GetComponent<TMP_Text>();
    }
        

    public void InitialiseGameScene() 
    {
       canvas = GameObject.Find("Canvas");
       pause = canvas.transform.Find("Pause").gameObject;
       gameOver = canvas.transform.Find("GameOver").gameObject;

       countdownText = canvas.transform.Find("Screen/CountdownText").GetComponent<TMP_Text>();
       pauseButton = canvas.transform.Find("Screen/SafeArea/PauseButton").GetComponent<Button>();
    }

    public void OpenMenu(Menus menus)
    {
        switch (menus)
        {
            case Menus.Settings:
                settings.SetActive(true);
                highScore.SetActive(false);
                screen.SetActive(false);
                break;
            case Menus.HighScore:
                highScore.SetActive(true);
                settings.SetActive(false);
                screen.SetActive(false);
                break;
            case Menus.MainMenu:
                settings.SetActive(false);
                highScore.SetActive(false);
                screen.SetActive(true);
                break;
            case Menus.Paused:
                pause.SetActive(true);
                gameOver.SetActive(false);
                break;
            case Menus.GameOver:
                pause.SetActive(false);
                gameOver.SetActive(true);
                break;
            case Menus.InGame:
                pause.SetActive(false);
                gameOver.SetActive(false);
                break;
        }
    }

    public GameObject GetObject(Menus menus)
    {

        switch (menus)
        {
            case Menus.Settings:
                return settings;

            case Menus.HighScore:
                return highScore;
        }

        return null;
    }

    public void AnimateMenu(Vector3 scale, Menus menu, float speed, Ease ease)
    {
        GameObject gameObject = GetObject(menu);
        gameObject.transform.DOScale(scale, speed).SetEase(ease);
    }

    public IEnumerator CountdownScreen(TMP_Text text)
    {
        Time.timeScale = 0.0f;

        pauseButton.interactable = false;

        GameManager.Instance.currentGameStates = GameStates.isStarted;

        SFXManager.Instance.PlaySound(SFXManager.Instance.countdownSound);

        UIManager.Instance.OpenMenu(Menus.InGame);

        text.gameObject.SetActive(true);

        int counter = 3;


        while (counter > 0)
        {
            text.text = counter.ToString();
            yield return new WaitForSecondsRealtime(1.0f);
            counter--;
        }

        text.text = "GO!";

        yield return new WaitForSecondsRealtime(1.0f);

        if(text != null) 
        {
            text.gameObject.SetActive(false);
        }

        GameManager.Instance.currentGameStates = GameStates.isPlaying;

        pauseButton.interactable = true;

        Time.timeScale = 1.0f;
    }

    public void StartCountdownCoroutine() 
    {
        StartCoroutine(CountdownScreen(UIManager.Instance.countdownText));
    }
}
