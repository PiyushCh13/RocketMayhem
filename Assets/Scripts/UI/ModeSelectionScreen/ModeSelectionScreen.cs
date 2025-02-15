using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSelectionScreen : MonoBehaviour
{
    public void OnClickBack() 
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        GameManager.Instance.LoadScene(SceneList.MainScreen.ToString());
        //MainMenu.OpenMenu(Menus.MainMenu);
    }

    public void OnClickCampaign() 
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        GameManager.Instance.LoadScene(SceneList.CharacterSelection.ToString());
    }

    public void OnClickLevelSelect() 
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        GameManager.Instance.LoadScene(SceneList.LevelSelection.ToString());
    }

    public void OnClickLevelEditor()
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        GameManager.Instance.LoadScene(SceneList.LevelEditor.ToString());
    }
}
