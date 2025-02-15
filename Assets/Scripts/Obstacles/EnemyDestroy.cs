using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Spaceship") 
        {
            GameManager.Instance.OnGameOver();
            Destroy(collision.gameObject);
        }
    }
}
