using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SH_UI_AliveEnemy : MonoBehaviour
{
    public Text text;
    public SH_StageManager sm;
    private void Start()
    {
        text = GetComponent<Text>();
        sm = FindObjectOfType<SH_StageManager>();
    }

    private void Update()
    {
        text.text = "Alive : " + sm.currentStage.aliveEnemy;
    }
}
