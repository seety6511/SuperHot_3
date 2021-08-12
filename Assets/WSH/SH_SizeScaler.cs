using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_SizeScaler : MonoBehaviour
{
    public float speed;
    public GameObject target;
    public bool up;
    public Vector3 minScale;
    public Vector3 maxScale;
    private void OnEnable()
    {
        if (up)
        {
            target.transform.localScale = Vector3.zero;
            iTween.ScaleTo(target, maxScale, speed);
        }
        else
        {
            iTween.ScaleTo(target, minScale, speed);
        }
        
    }
}
