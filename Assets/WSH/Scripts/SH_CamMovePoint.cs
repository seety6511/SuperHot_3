using System.Collections.Generic;
using UnityEngine;
public class SH_CamMovePoint : MonoBehaviour
{
    public List<Transform> points;
    public int pointIndex;
    public bool isSetting;
    public float speed = 1f;
    public bool isEnd;
    public void FirstSetting()
    {
        isEnd = false;
        pointIndex = 0;
        Camera.main.gameObject.transform.position = points[pointIndex].position;
        Camera.main.gameObject.transform.rotation = points[pointIndex].rotation;
        isSetting = true;
    }

    public void MoveTo()
    {
        if (pointIndex + 1 == points.Count)
            return;
        var cam = Camera.main.gameObject.GetComponent<iTweenPositionTo>();
        cam.valueFrom= points[pointIndex].position;
        cam.valueTo = points[pointIndex + 1].position;
        cam.tweenTime = speed;
        cam.iTweenPlay();
        pointIndex++;
    }
}