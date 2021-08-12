using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_BulletFactory : MonoBehaviour
{
    static SH_BulletFactory instance;
    public static SH_BulletFactory Instance { get => instance; }
    static SH_BulletFactory() { }

    public List<GameObject> bulletPool = new List<GameObject>();
    public List<GameObject> usedBulletPool = new List<GameObject>();
    public List<GameObject> bulletTrailPool = new List<GameObject>();
    public List<GameObject> usedTrailPool = new List<GameObject>();
    public int bulletPoolSize = 100;
    public GameObject bulletPrefab;
    public GameObject bulletTrailPrefab;
    void Start()
    {
        instance = this;
        for(int i = 0; i < bulletPoolSize; ++i)
        {
            var b = Instantiate(bulletPrefab);
            b.SetActive(false);
            b.transform.SetParent(transform);

            var t = Instantiate(bulletTrailPrefab);
            t.transform.SetParent(transform);
            //t.GetComponent<SH_BulletTrail>().trail.emitting = false;
            t.SetActive(false);
        }
    }
    
    public void PushTrail(GameObject trail)
    {
        Instance.usedTrailPool.Remove(trail);
        Instance.bulletTrailPool.Add(trail);
    }

    public void PushBullet(GameObject bullet)
    {
        Instance.usedBulletPool.Remove(bullet);
        Instance.bulletPool.Add(bullet);
    }

    public SH_BulletTrail Pull_Trail(Transform trs, Vector3 pos)
    {
        GameObject trail = GetTrail();

        if (trail == null)
            return null;
        trail.transform.SetPositionAndRotation(pos, Quaternion.identity);
        trail.GetComponent<SH_BulletTrail>().SetBullet(trs);
        trail.SetActive(true);
        return trail.GetComponent<SH_BulletTrail>();
    }
    //플레이어 전용
    public SH_Bullet BulletPull(Transform startPos, Vector3 dir )
    {
        GameObject bullet = GetBullet();

        if(bullet==null)
        {
            Debug.Log("All Pool Used");
            return null;
        }

        bullet.GetComponent<SH_Bullet>().Shoot(startPos, dir);
        bullet.SetActive(true);

        return bullet.GetComponent<SH_Bullet>();
    }

    //에너미전용
    public SH_Bullet BulletPull(GameObject target, Transform startPos)
    {
        GameObject bullet = GetBullet();

        if (bullet == null)
        {
            Debug.Log("All Pool Used");
            return null;
        }
        bullet.GetComponent<SH_Bullet>().Shoot(target, startPos);
        bullet.SetActive(true);
        return bullet.GetComponent<SH_Bullet>();
    }

    GameObject GetBullet()
    {
        if (bulletPool.Count == 0)
            return null;

        var result = bulletPool[0];
        usedBulletPool.Add(result);
        bulletPool.RemoveAt(0);
        return result;
    }

    GameObject GetTrail()
    {
        if (bulletTrailPool.Count == 0)
            return null;

        var result = bulletTrailPool[0];
        usedTrailPool.Add(result);
        bulletTrailPool.RemoveAt(0);
        return result;
    }
}
