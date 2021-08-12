using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SH_StartSceneEventManager : MonoBehaviour
{
    public void PlayButtonEvent()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void ExitButtonEvent()
    {
        Application.Quit();
    }
}
