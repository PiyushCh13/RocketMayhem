using UnityEngine;

public static class SaveLoadSystem
{
    public static void InitialiseData()
    {
        if (!PlayerPrefs.HasKey("HIGH_SCORE"))
        {
            SaveData saveData = new SaveData
            {
                HIGH_SCORE = 0,
                SFX_SLIDER = 0.5f,
                MUSIC_SLIDER = 0.5f
            };
            SaveData(saveData);
        }
    }

    public static void SaveData(SaveData data)
    {
        PlayerPrefs.SetInt("HIGH_SCORE", data.HIGH_SCORE);
        PlayerPrefs.SetFloat("SFX_SLIDER", data.SFX_SLIDER);
        PlayerPrefs.SetFloat("MUSIC_SLIDER", data.MUSIC_SLIDER);
        PlayerPrefs.Save();
    }

    public static SaveData LoadData()
    {
        if (PlayerPrefs.HasKey("HIGH_SCORE"))
        {
            SaveData data = new SaveData
            {
                HIGH_SCORE = PlayerPrefs.GetInt("HIGH_SCORE"),
                SFX_SLIDER = PlayerPrefs.GetFloat("SFX_SLIDER"),
                MUSIC_SLIDER = PlayerPrefs.GetFloat("MUSIC_SLIDER")
            };
            return data;
        }
        else
        {
            return null;
        }
    }
}
