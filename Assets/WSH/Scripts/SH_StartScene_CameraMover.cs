using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_StartScene_CameraMover : MonoBehaviour
{
    public List<SH_CamMovePoint> camMovePoints;
    public SH_CamMovePoint currentMovingPoint;
    public int camMovePointIndex;
    public void CamMove()
    {
        if (currentMovingPoint.isEnd)
            NextCamMovePoint();

        if (!currentMovingPoint.isSetting)
            currentMovingPoint.FirstSetting();

        currentMovingPoint.MoveTo();
    }

    void NextCamMovePoint()
    {
        currentMovingPoint = camMovePoints[camMovePointIndex++];

        if (camMovePointIndex == camMovePoints.Count)
            camMovePointIndex = 0;

        if (currentMovingPoint.isEnd)
            currentMovingPoint.FirstSetting();
    }

    private void Start()
    {
        currentMovingPoint = camMovePoints[0];
        CamMove();
        camMovePointIndex++;
    }

    private void Update()
    {
        if (currentMovingPoint.isEnd)
        {
            NextCamMovePoint();
            CamMove();
        }
    }
    public void OnStart()
    {
    }
    public void OnComplete()
    {
        if (currentMovingPoint.pointIndex + 1 == currentMovingPoint.points.Count)
            currentMovingPoint.isEnd = true;
        else if (currentMovingPoint.pointIndex + 1 < currentMovingPoint.points.Count)
            currentMovingPoint.MoveTo();
    }
}
