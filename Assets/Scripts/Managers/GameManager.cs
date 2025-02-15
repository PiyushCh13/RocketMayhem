using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneList
{
    MainScreen,
    ModeSelectionScene,
    CharacterSelection,
    GameScene,
    LevelEditor,
    LevelSelection
}

public enum GameStates
{
    isStarted,
    isPlaying,
    isPaused,
    GameOver,
    inMenu
}

public class GameManager : Singleton<GameManager>
{
    [Header("Player")]

    public GameObject spaceShip;
    public Vector3 startPos;
    SpriteRenderer playerRenderer;
    public Sprite playerSprite;
    public Sprite defaultSprite;
    public bool hasInstantiatedPlayer ;

    [Header("Gameplay")]
    private float score = 0;
    public float Score { get { return score; } set { score = value; } }


    private int highscore = 0;
    public int HighScore { get { return highscore; } set { highscore = value; } }

    public GameStates currentGameStates;
    public float mins;
    public float seconds;
    public float elapsedTime;
    public GameObject explosion_Effect;

    void Start()
    {
       SaveLoadSystem.InitialiseData();

       currentGameStates = GameStates.inMenu;

       hasInstantiatedPlayer = false;

       LoadGame();
    }

    void Update()
    {
        if(spaceShip == null && !hasInstantiatedPlayer) PlayerSpriteChange();
    }

    public void PlayerSpriteChange() 
    {
        if(SceneManager.GetActiveScene().name == SceneList.GameScene.ToString())
        {
            spaceShip = Instantiate(Resources.Load("Spaceship") as GameObject, startPos, Quaternion.identity);
            spaceShip.gameObject.name = "Spaceship";
            playerRenderer = spaceShip.GetComponent<SpriteRenderer>();
            playerRenderer.sprite = playerSprite;

            if(playerSprite == null) 
            {
                playerRenderer.sprite = defaultSprite;
            }
        }
    }

    public void LoadScene(string sceneName)
    {
        Scene prevScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(sceneName,LoadSceneMode.Single);

        if (prevScene != SceneManager.GetActiveScene() && prevScene != null)
        {
            SceneManager.UnloadSceneAsync(prevScene);
        }
    }

    public void ResetScoreandTime() 
    {
        Score = 0;
        mins = 0;
        seconds = 0;
        elapsedTime = 0;
    }

    public void OnGameOver() 
    {
        if(score > highscore) 
        {
            highscore = Mathf.FloorToInt(score);
            GameManager.Instance.SaveGame();
        }

        SFXManager.Instance.PlaySound(SFXManager.Instance.explosionSound);
        Instantiate(explosion_Effect, spaceShip.transform.position , Quaternion.identity);
        currentGameStates = GameStates.GameOver;
        UIManager.Instance.OpenMenu(Menus.GameOver);
        hasInstantiatedPlayer = true;
        Time.timeScale = 0;
    }

    public void SaveGame() 
    {
        SaveData saveData = new SaveData
        {
            HIGH_SCORE = HighScore,
            SFX_SLIDER = MusicManager.Instance.musicSource.volume,
            MUSIC_SLIDER = SFXManager.Instance.sfxAudioSource.volume
        };

        string json = JsonUtility.ToJson(saveData);
        SaveLoadSystem.SaveData(json);
    }

    public void LoadGame() 
    {
        string saveString = SaveLoadSystem.LoadData();

        if (saveString != null)
        {
            SaveData savedata = JsonUtility.FromJson<SaveData>(saveString);

            HighScore = savedata.HIGH_SCORE;
            MusicManager.Instance.musicSource.volume = savedata.MUSIC_SLIDER;
            SFXManager.Instance.sfxAudioSource.volume = savedata.SFX_SLIDER;
        }
    }

}
