using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    void Start()
    {
        
    }

    
    void Update()
    {
        if (this.gameObject != null)
        {
            EnemyFuntion();
        }
    }

    public void EnemyFuntion()
    {
        transform.DORotate(new Vector3(0,0,360f),8f,RotateMode.WorldAxisAdd).SetLoops(-1 ,LoopType.Incremental);
    }

    private void OnDisable()
    {
        DOTween.ClearCachedTweens();
    }
}
