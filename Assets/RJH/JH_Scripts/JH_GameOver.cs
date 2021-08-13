using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JH_GameOver : MonoBehaviour
{
    public GameObject image;
    public GameObject blood1;
    public GameObject blood2;
    public float bloodTweenSize;
    public float bloodTweenTime;

    void OnEnable()
    {   //ÇöÀç stageUI
        //currStageUI.text = "Stage :" + SH_Stage.;
        //ScoreUI
        //ScoreUI.text = "Score : " + 0;
        blood1.transform.localScale = Vector3.zero;
        blood2.transform.localScale = Vector3.zero;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        iTween.ScaleTo(blood1, iTween.Hash("x", bloodTweenSize, "y", bloodTweenSize, "z", bloodTweenSize, "time", bloodTweenTime,
                                           "easetype", iTween.EaseType.easeOutExpo));
        iTween.ScaleTo(blood2, iTween.Hash("x", bloodTweenSize, "y", bloodTweenSize, "z", bloodTweenSize, "time", bloodTweenTime,
                                           "easetype", iTween.EaseType.easeOutExpo)) ;
    }

    public void OnClickRetry()
    {
        SH_TimeScaler.ReplayTime();
        FindObjectOfType<SH_StageManager>().inputWaiting = false;
        FindObjectOfType<SH_StageManager>().CurrentStageReloading();
        gameObject.SetActive(false);
    }

    public void OnclickHome()
    {
        SceneManager.LoadScene("StartScene");
    }
}
