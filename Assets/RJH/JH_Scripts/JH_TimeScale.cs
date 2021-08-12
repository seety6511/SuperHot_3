using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_TimeScale : MonoBehaviour
{
    private float slowMo = 0.1f;
    private float currentTime = 1.0f;
    private bool doSlowMo = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {

            Time.timeScale = slowMo;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            doSlowMo = false;
        }
        
        else
        {
            if (!doSlowMo)
            {
            Time.timeScale = currentTime;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            doSlowMo = false;
            
            }
        }
    }
}