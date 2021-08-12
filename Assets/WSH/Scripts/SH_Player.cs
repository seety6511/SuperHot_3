using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_Player : MonoBehaviour
{
    public Transform head;
    public Transform neck;
    public Transform torso;
    public Transform leg;
    public GameObject weapon;

    public bool isDead;
    public bool overpower;
    public GameObject overPowerUI;

    public Transform firePos;
    public GameObject crossHead;
    public Vector3 crossHeadPoint; //화면 정 가운데. 총알이 날아가는 방향.

    public Camera cam;
    public Animator animator;
    public Transform deadCamPos;
    public AudioSource audioSource;
    public SH_StageManager stageManager;
    public SH_PlayerMove pm;
    public Transform GetRandomTargetPart()
    {
        var r = Random.Range(0, 3);

        switch (r)
        {
            case 0:
                return head;
            case 1:
                return torso;
            case 2:
                return leg;
        }

        return null;
    }

    private void Start()
    {
        stageManager = FindObjectOfType<SH_StageManager>();
        Init();
    }

    public void Init()
    {
        audioSource.Stop();
        isDead = false;
        animator.SetLayerWeight(1, 1f);
        animator.SetBool("Revive",true);
        pm.Init();
        crossHeadPoint = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
        crossHead.transform.position = crossHeadPoint;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if (isDead)
            return;

        Fire();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!overpower)
            {
                overpower = true;
                overPowerUI.SetActive(true);
            }
            else
            {
                overpower = false;
                overPowerUI.SetActive(false);
            }
        }
    }
    Ray ray;
    Vector3 crossPoint;
    bool fire;
    void Fire()
    {
        ray = Camera.main.ScreenPointToRay(crossHeadPoint);
        RaycastHit hit;
        if (Physics.Raycast(firePos.position, ray.direction, out hit,Mathf.Infinity,1<<0))
        {
            if (hit.point != crossPoint)
                crossPoint = hit.point;
        }

        crossHead.transform.position = crossPoint;
        crossHead.transform.rotation = Quaternion.LookRotation(ray.direction);
        if (!fire)
            weapon.transform.rotation = Quaternion.LookRotation(ray.direction);
        if (Input.GetButtonDown("Fire1"))
        {
            fire = true;
            SH_BulletFactory.Instance.BulletPull(firePos, ray.direction);
            firePos.gameObject.GetComponent<AudioSource>().PlayOneShot(SH_SoundContainer.Instance.shotSound);
            animator.SetTrigger("Attack");
        }
    }

    void Aim() { }

    void Shoot()
    {
        fire = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(firePos.position, ray.direction*Mathf.Infinity);
    }
    public void Hit()
    {
        if (overpower|| isDead)
            return;

        animator.SetBool("Revive", false);
        animator.SetLayerWeight(1, 0f);
        isDead = true;
        iTween.LookTo(cam.gameObject, gameObject.transform.position, 0.1f);
        iTween.MoveTo(cam.gameObject, deadCamPos.position, 12f);
        animator.SetTrigger("Death");
        audioSource.PlayOneShot(SH_SoundContainer.Instance.entityHitSound);
        audioSource.PlayOneShot(SH_SoundContainer.Instance.playerDeadBGM);
    }

    void Die()
    {
    }
}
