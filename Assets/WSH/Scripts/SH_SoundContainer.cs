using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_SoundContainer : MonoBehaviour
{
    static SH_SoundContainer instance;
    public static SH_SoundContainer Instance { get => instance; }
    static SH_SoundContainer() { }

    public AudioClip bulletImpact;  //�繰�� �Ѿ��� �ε�����
    public AudioClip shotSound;     //�� �߻�� �Ҹ�
    public AudioClip timeStopBGM;   //�ð� ���ӽ�
    public AudioClip playerDeadBGM; //�÷��̾� �����
    public AudioClip entityHitSound;//��ƼƼ�� �ѿ� �¾��� ���
    public AudioClip reloadSound;   //�÷��̾� ������ �Ҹ�
    void Start()
    {
        instance = this;
    }
}
