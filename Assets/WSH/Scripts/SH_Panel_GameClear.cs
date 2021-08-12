using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SH_Panel_GameClear : MonoBehaviour
{
    private void OnEnable()
    {
        SH_TimeScaler.StopTime();
    }
    void Update()
    {        

    }
}
