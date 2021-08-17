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
        StartCoroutine("co");
    }

    IEnumerator co()
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0f;
        //SH_TimeScaler.StopTime();
    }

    public void ResumeButton()
    {
        Time.timeScale =1f;
        //SH_TimeScaler.ReplayTime();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        FindObjectOfType<SH_StageManager>().inputWaiting = false;
        gameObject.SetActive(false);
    }

    public void HomeButton()
    {
        Time.timeScale = 1f;
        //SH_TimeScaler.ReplayTime();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("StartScene");
    }
}
