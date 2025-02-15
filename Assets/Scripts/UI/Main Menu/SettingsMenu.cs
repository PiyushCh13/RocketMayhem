using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        musicSlider = transform.Find("SettingsBG/VolumeSlider").GetComponent<Slider>();
        sfxSlider = transform.Find("SettingsBG/SfxSlider").GetComponent<Slider>();

        musicSlider.value = MusicManager.Instance.musicSource.volume;
        sfxSlider.value = SFXManager.Instance.sfxAudioSource.volume;
    }
    public void OnClose()
    {
        GameManager.Instance.SaveGame();
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        UIManager.Instance.OpenMenu(Menus.MainMenu);
        transform.DOScale(Vector3.zero , 1f);
    }
    public void MusicSlider() 
    {
        MusicManager.Instance.musicSource.volume = musicSlider.value;
    }

    public void SfxSlider()
    {
        SFXManager.Instance.sfxAudioSource.volume = sfxSlider.value;
    }
}
