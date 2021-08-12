using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SH_Panel_GameOver : MonoBehaviour
{
    public Text currentStage;
    public Text score;
    public GameObject restartButton;
    private void OnEnable()
    {
        SH_TimeScaler.StopTime();
        if (FindObjectOfType<SH_StageManager>().infinityMode)
            restartButton.SetActive(false);
        else
            restartButton.SetActive(true);
    }

}
