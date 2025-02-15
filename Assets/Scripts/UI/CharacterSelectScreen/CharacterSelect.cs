using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public Sprite selectedImage;

    public void OnClickSpaceship()
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        selectedImage = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite;
        GameManager.Instance.playerSprite = selectedImage;
        GameManager.Instance.LoadScene(SceneList.GameScene.ToString());
    }
}
