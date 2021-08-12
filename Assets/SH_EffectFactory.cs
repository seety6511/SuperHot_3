using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_EffectFactory : MonoBehaviour
{
    static SH_EffectFactory instance;
    public static SH_EffectFactory Instance => instance;
    static SH_EffectFactory() { }

    public GameObject entityHitEffect;  //총알이 플레이어, 혹은 적에게 맞았을때 생성될 이펙트
    public GameObject bulletEnvImapact; //총알이 환경오브젝트와 충돌했을때 생성할 이펙트

    public int effectPoolSize;

    public List<GameObject> ehEffectPool;
    public List<GameObject> beEffectPool;

    private void Start()
    {
        instance = this;

        //for(int i = 0; i < effectPoolSize; ++i)
        //{
        //    var e = Instantiate(entityHitEffect);
        //    e.SetActive(false);
        //    ehEffectPool.Add(e);

        //    var e2 = Instantiate(bulletEnvImapact);
        //    e2.SetActive(false);

        //}
    }

}
