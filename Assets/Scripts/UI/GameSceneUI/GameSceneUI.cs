using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSceneUI : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text timeText;

    private void Awake()
    {
        UIManager.Instance.InitialiseGameScene();
    }

    public void PauseMenu() 
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        Time.timeScale = 0.0f;
        GameManager.Instance.currentGameStates = GameStates.isPaused;
        UIManager.Instance.OpenMenu(Menus.Paused);
    }

    private void Update()
    {
        if(GameManager.Instance.currentGameStates == GameStates.isPlaying)
        {
            UpdateScoreAndTime(scoreText, timeText);
        }
    }

    public void UpdateScoreAndTime(TMP_Text scoreText, TMP_Text timeText)
    {
        GameManager.Instance.Score += 10 * Time.deltaTime;

        scoreText.text = "SCORE: " + ((int)GameManager.Instance.Score).ToString();

        GameManager.Instance.elapsedTime += Time.deltaTime;

        GameManager.Instance.mins = (Mathf.FloorToInt(GameManager.Instance.elapsedTime / 60));

        GameManager.Instance.seconds = (Mathf.FloorToInt(GameManager.Instance.elapsedTime % 60));

        if (GameManager.Instance.seconds >= 0 && GameManager.Instance.seconds <= 9)
        {
            timeText.text = "TIME: 0" + GameManager.Instance.mins + ":" + "0" + GameManager.Instance.seconds;
        }

        else
        {
            timeText.text = "TIME: 0" + GameManager.Instance.mins + ":" + GameManager.Instance.seconds;
        }

        if (GameManager.Instance.mins > 9 && GameManager.Instance.seconds > 9)
        {
            timeText.text = "TIME: " + GameManager.Instance.mins + ":" + GameManager.Instance.seconds;
        }

        if (GameManager.Instance.mins >= 9 && GameManager.Instance.seconds <= 9)
        {
            timeText.text = "TIME: " + GameManager.Instance.mins + ":0" + GameManager.Instance.seconds;
        }
    }
}
