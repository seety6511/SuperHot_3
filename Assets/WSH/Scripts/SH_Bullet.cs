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

    RaycastHit frontHit;
    RaycastHit backHit;
    public string frontHitTag;
    public string backHitTag;
    private void Update()
    {
        if (Vector3.Distance(transform.position, startPos) >= maxRange)
            gameObject.SetActive(false);

        if (Physics.Raycast(transform.position, transform.forward, out frontHit))
        {
            if (frontHit.transform.tag == "Player")
            {
                //print(frontHit.distance);
                if (frontHit.distance < 2)
                {
                    frontHit.collider.gameObject.GetComponent<SH_Player>().Hit();
                    HitEffect(entityHitEffect);
                    gameObject.SetActive(false);
                    //print("플레이어 맞음");
                }
            }
            else if (frontHit.transform.tag == "Enemy")
            {
                //print(frontHit.distance);
                if (frontHit.distance < 2)
                {
                    if (frontHit.collider.gameObject.GetComponent<SH_Enemy>() != null)
                    {
                        frontHit.collider.gameObject.GetComponent<SH_Enemy>().Hit();
                        HitEffect(entityHitEffect);
                        gameObject.SetActive(false);

                    }
                    //print("플레이어 맞음");
                }
            }
            else if (frontHit.transform.tag == "Wall")
            {
                //print(frontHit.distance);
                if (frontHit.distance < 2)
                {
                    HitEffect(wallHitEffect);
                    gameObject.SetActive(false);
                    //print("플레이어 맞음");
                }
            }
        }

        gameObject.transform.Translate(dir * speed * SH_TimeScaler.deltaTime * SH_TimeScaler.TimeScale, Space.World);

        //if (Physics.Raycast(transform.position, transform.forward, out frontHit, 1f))
        //{
        //    frontHitTag = frontHit.collider.gameObject.tag;
        //}
        //else
        //{
        //    if (Physics.Raycast(transform.position, -transform.forward, out backHit, 1f))
        //    {
        //        backHitTag = backHit.collider.gameObject.tag;

        //        //Debug.Log("F : " + frontHitTag);
        //        //Debug.Log("B : " + backHitTag);
        //        if (frontHitTag == backHitTag)
        //        {
        //            if (backHitTag == "Enemy")
        //            {
        //                backHit.collider.gameObject.GetComponent<SH_Enemy>().Hit();
        //                HitEffect(entityHitEffect);
        //            }
        //            else if (backHitTag == "Player")
        //            {
        //                backHit.collider.gameObject.GetComponent<SH_Player>().Hit();
        //                HitEffect(entityHitEffect);
        //            }
        //            else if (backHitTag == "Wall")
        //            {
        //                HitEffect(wallHitEffect);
        //            }
        //            gameObject.SetActive(false);
        //        }
        //    }
        //}
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward);
        Gizmos.DrawRay(transform.position, -transform.forward);
    }
    //private void Update()
    //{
        //if (Vector3.Distance(transform.position, startPos) >= maxRange)
        //    gameObject.SetActive(false);

        ////gameObject.transform.Translate(dir * speed * SH_TimeScaler.deltaTime * SH_TimeScaler.TimeScale, Space.World);
        //rb.position += dir * speed * SH_TimeScaler.deltaTime * SH_TimeScaler.TimeScale;
    //}

    void HitEffect(GameObject particle)
    {
        var e = Instantiate(particle);
        e.transform.position = frontHit.point;
        e.transform.rotation = Quaternion.LookRotation(-dir);
        e.GetComponent<ParticleSystem>().Play();
        Destroy(e.gameObject, 12f);
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("A");
        //if (other.transform.CompareTag("Player"))
        //{
        //    HitEffect(entityHitEffect);

        //    other.gameObject.GetComponent<SH_Player>().Hit();
        //    gameObject.SetActive(false);
        //}
        //else if (other.transform.CompareTag("Wall"))
        //{
        //    HitEffect(wallHitEffect);
        //    gameObject.SetActive(false);
        //}
        //else if (other.transform.CompareTag("Enemy"))
        //{
        //    HitEffect(entityHitEffect);

        //    other.gameObject.GetComponent<SH_Enemy>().Hit();
        //    gameObject.SetActive(false);
        //}
    }
    void OnDisable()
    {
        frontHitTag = "";
        SH_BulletFactory.Instance.PushBullet(gameObject);
    }
}
