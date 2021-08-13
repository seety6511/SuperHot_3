using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SH_Panel_GameClear : MonoBehaviour
{
    public Button infinityMode;

    private void OnEnable()
    {
        SH_TimeScaler.StopTime();
    }
    public void InfinityModeOn()
    {
        FindObjectOfType<SH_StageManager>().InfiniteMode();
    }
}
