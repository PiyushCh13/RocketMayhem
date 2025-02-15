using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class FPSManager : MonoBehaviour
{
    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;
    }
}
