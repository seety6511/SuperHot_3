using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_EffectFactory : MonoBehaviour
{
    static SH_EffectFactory instance;
    public static SH_EffectFactory Instance => instance;
    static SH_EffectFactory() { }

    public GameObject entityHitEffect;  //�Ѿ��� �÷��̾�, Ȥ�� ������ �¾����� ������ ����Ʈ
    public GameObject bulletEnvImapact; //�Ѿ��� ȯ�������Ʈ�� �浹������ ������ ����Ʈ

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
