using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SH_UI_PauseMenu : MonoBehaviour
{
    public Button resume;
    public Button home;

    private void OnEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SH_TimeScaler.StopTime();
    }

    public void ResumeButton()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        FindObjectOfType<SH_StageManager>().inputWaiting = false;
        SH_TimeScaler.ReplayTime();
        gameObject.SetActive(false);
    }

    public void HomeButton()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SH_TimeScaler.ReplayTime();
        SceneManager.LoadScene("StartScene");
    }

}
