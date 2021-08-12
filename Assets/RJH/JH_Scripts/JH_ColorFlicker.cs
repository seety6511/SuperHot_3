using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JH_ColorFlicker : MonoBehaviour
{
    public Image lamp;
    private Color originColor;
    public float speed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        //램프 오브젝트의 색상정보를 저장
        originColor = lamp.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        //시간변화를 곱
        float flicker = Mathf.Abs(Mathf.Sin(Time.time * speed));
        lamp.material.color = originColor * flicker;

    }
}
