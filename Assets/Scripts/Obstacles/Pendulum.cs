using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(this.gameObject != null) 
        {
            EnemyFuntion(-20);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyFuntion(float amount)
    {
        transform.DORotate(new Vector3(0, 0, amount ), 1f, RotateMode.Fast).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    private void OnDisable()
    {
        DOTween.ClearCachedTweens();
    }
}
