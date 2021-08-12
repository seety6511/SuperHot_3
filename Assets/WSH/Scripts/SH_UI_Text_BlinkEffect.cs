using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�� ������Ʈ�� ������ Text UI�� �����̴� ȿ���� �ο��Ѵ�.
public class SH_UI_Text_BlinkEffect : MonoBehaviour
{
    public bool effectOn;   // ȿ���� ������ �������� ���� ����
    public bool isGradation;    // �׶��̼����� �����?
    public float blinkOnTimer = 1f;  //�ִ�� ������������ �����ϴ� �ð�.
    public float blinkOffTimer = 0.6f; //�ִ�� �帴�������� �����ϴ� �ð�
    public float gradationTimer = 3f;
    [Range(0f,1f)]
    public float onAlpha;
    [Range(0f,1f)]
    public float offAlpha;
    public Text target;

    float onTimer;
    float offTimer;
    bool isOn;
    void Start()
    {
        target = gameObject.GetComponent<Text>();
        onTimer = 0f;
        offTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!effectOn)
            return;

        if (isGradation)
        {
            var nc = target.color;
            if (isOn)
            {
                onTimer += Time.deltaTime;
                if (onTimer >= gradationTimer)
                {
                    onTimer = 0f;
                    isOn = false;
                }
                nc.a = Mathf.Max(nc.a - Time.deltaTime*0.5f, offAlpha);
            }
            else
            {
                offTimer += Time.deltaTime;
                if (offTimer >= gradationTimer)
                {
                    offTimer = 0f;
                    isOn = true;
                }
                nc.a = Mathf.Min(nc.a + Time.deltaTime*0.5f, onAlpha);
            }
            target.color = nc;
            return;
        }

        if (isOn)
        {
            onTimer += Time.deltaTime;
            if (onTimer >= blinkOnTimer)
            {
                onTimer = 0f;
                isOn = false;
                TextOff();
            }
        }
        else
        {
            offTimer += Time.deltaTime;
            if (offTimer >= blinkOffTimer)
            {
                offTimer = 0f;
                isOn = true;

                TextOn();
            }
        }
    }

    void TextOff()
    {
        var nc = target.color;
        nc.a = offAlpha;
        target.color = nc;
    }

    void TextOn()
    {
        var nc = target.color;
        nc.a = onAlpha;
        target.color = nc;
    }
}
