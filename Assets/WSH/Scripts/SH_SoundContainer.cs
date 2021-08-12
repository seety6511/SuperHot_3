using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_SoundContainer : MonoBehaviour
{
    static SH_SoundContainer instance;
    public static SH_SoundContainer Instance { get => instance; }
    static SH_SoundContainer() { }

    public AudioClip bulletImpact;  //사물과 총알이 부딪힐때
    public AudioClip shotSound;     //총 발사시 소리
    public AudioClip timeStopBGM;   //시간 감속시
    public AudioClip playerDeadBGM; //플레이어 사망시
    public AudioClip entityHitSound;//엔티티가 총에 맞았을 경우
    public AudioClip reloadSound;   //플레이어 재장전 소리
    void Start()
    {
        instance = this;
    }
}
