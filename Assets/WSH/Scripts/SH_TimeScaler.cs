using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_TimeScaler : MonoBehaviour
{
    [Range(0, 1)]
    public float _timeScale;
    [Range(1,10)]
    public float _originScale;

    static float timeScale;
    public AudioSource audioSource;
    public static float TimeScale
    {
        get
        {
            if (scaleOn)
                return timeScale;
            else
                return originScale;
        }
    }
    static float originScale;

    public static float deltaTime;
    public static bool scaleOn;
    // Update is called once per frame

    private void Start()
    {
        timeScale = _timeScale;
        originScale = _originScale;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.Play();
            scaleOn = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            audioSource.Stop();
            scaleOn = false;
        }

        deltaTime = Time.deltaTime * TimeScale;
    }

    public static void StopTime()
    {
        scaleOn = true;
        //timeScale = 0f;
    }
    public static void ReplayTime()
    {
        scaleOn = false;
        //timeScale = originScale;
    }
}
