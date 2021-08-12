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
        //���� ������Ʈ�� ���������� ����
        originColor = lamp.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        //�ð���ȭ�� ��
        float flicker = Mathf.Abs(Mathf.Sin(Time.time * speed));
        lamp.material.color = originColor * flicker;

    }
}
