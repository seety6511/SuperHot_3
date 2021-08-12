using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_BulletTrail : MonoBehaviour
{
    public float lifeTime;      //트레일이 생존한 시간. 불렛이 어딘가 피격되면 급격하게 감소한다.
    public TrailRenderer trail;
    Transform bullet;

    private void OnDisable()
    {
        SH_BulletFactory.Instance.PushTrail(gameObject);
    }
    private void OnEnable()
    {
        //trail.Clear();
        trail.time = lifeTime;
    }
    public void SetBullet(Transform obj)
    {
        bullet = obj;
    }

    float timer;
    private void Update()
    {

        if (trail.time <= 0f)
        {
            gameObject.SetActive(false);
            return;
        }
        if (!bullet.gameObject.activeSelf)
        {
            trail.time -= SH_TimeScaler.deltaTime;
            return;
        }
        gameObject.transform.position = bullet.position;
    }
}
