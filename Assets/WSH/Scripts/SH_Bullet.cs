using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_Bullet : MonoBehaviour
{
    public Transform target;
    public GameObject entityHitEffect;
    public GameObject wallHitEffect;
    public float speed;
    public float maxRange;  //이 이상 날아가면 오브젝트가 삭제된다. 기준은 shooter의 위치
    public Vector3 dir; //총알이 날아가는 방향
    public Vector3 startPos;

    void Shoot(Transform firePos)
    {
        gameObject.transform.position = firePos.position;
        startPos = gameObject.transform.position;
        SH_BulletFactory.Instance.Pull_Trail(transform, firePos.position);
        dir.Normalize();
    }
    public void Shoot(GameObject target, Transform startPos)
    {
        Shoot(startPos);
        gameObject.transform.LookAt(target.transform);
        dir = target.transform.position - transform.position;
    }

    public void Shoot(Transform firePos, Vector3 dir)
    {
        Shoot(firePos);
        gameObject.transform.rotation = Quaternion.LookRotation(dir);
        this.dir = dir;
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, startPos) >= maxRange)
            gameObject.SetActive(false);

        gameObject.transform.Translate(dir * speed * SH_TimeScaler.deltaTime * SH_TimeScaler.TimeScale, Space.World);
    }

    void HitEffect(GameObject particle)
    {
        var e = Instantiate(particle);
        e.transform.position = gameObject.transform.position;
        e.transform.rotation = Quaternion.LookRotation(-dir);
        e.GetComponent<ParticleSystem>().Play();
        Destroy(e.gameObject, 12f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            HitEffect(entityHitEffect);

            other.gameObject.GetComponent<SH_Player>().Hit();
            gameObject.SetActive(false);
        }
        else if (other.transform.CompareTag("Wall"))
        {
            HitEffect(wallHitEffect);
            gameObject.SetActive(false);
        }
        else if(other.transform.CompareTag("Enemy"))
        {
            HitEffect(entityHitEffect);

            other.gameObject.GetComponent<SH_Enemy>().Hit();
            gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
    }
    void OnDisable()
    {
        SH_BulletFactory.Instance.PushBullet(gameObject);
    }
}
