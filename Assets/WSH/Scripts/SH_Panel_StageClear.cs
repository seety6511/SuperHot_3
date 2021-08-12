using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_Panel_StageClear : MonoBehaviour
{
    private void OnEnable()
    {
        SH_TimeScaler.StopTime();
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            SH_TimeScaler.ReplayTime();
            FindObjectOfType<SH_StageManager>().inputWaiting = false;

            gameObject.SetActive(false);
        }
    }
}
