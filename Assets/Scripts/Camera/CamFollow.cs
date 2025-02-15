using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float Xoffset;

    private void Start()
    {

    }

    private void LateUpdate()
    {
        if (GameManager.Instance.spaceShip != null && player == null)
        {
            player = GameManager.Instance.spaceShip.transform;
        }

        if(player != null) 
        {
            PlayerFollow();
        }
    }

    private void PlayerFollow()
    {
        Vector3 playerFollow = new Vector3(player.position.x - Xoffset,transform.position.y,-10);
        transform.position = playerFollow;

    }
}
