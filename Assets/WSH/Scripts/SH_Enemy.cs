using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SH_Enemy : MonoBehaviour
{
    #region Variables
    public float moveSpeed; //이동속도 
    public float animSpeed = 1f; //애니메이션 기본 속도
    public float attackRange;   //공격을 하기 위해 필요한 최소 사거리
    public float attackCoolTime;    //다음 공격을 하기 위한 시간
    public float attackTimer;   //그냥 공격 타이머
    public float rotateSpeed = 5f;

    public bool isAiming;   //조준중일때
    public bool isDead;
    public bool moveTo;     //지정 위치로 이동중일때
    public int moveOrder;   //이동 위치 지정. movePointList와 함께 사용
    public List<Transform> movePointList = new List<Transform>();   //이동해야할 위치

    public Transform shootPos;  //총알이 발사되는 위치
    public NavMeshAgent agent;
    public Animator animator;
    public SH_Player target;
    public GameObject weapon;
    public Transform head;

    public AudioSource hitSound;
    public AudioSource footSound;
    #endregion

    #region Unity Methods
    private void Start()
    {
        target = FindObjectOfType<SH_Player>();
        agent.speed = moveSpeed;
        attackTimer = 0f;
        isAiming = false;
    }

    //1. 공격범위에 들었는지 체크
    //2. 공격범위에 들었다면 Chase
    //3. 공격범위 안에 없다면 MoveToPoint
    //4. MoveToPoint 중 공격범위 내에 들어온다면 Chase로 전환
    //5. 모든 MovePoint를 순회했다면 Chase로 전환
    private void Update()
    {
        animator.speed = animSpeed * SH_TimeScaler.TimeScale;

        if (isDead)
            return;

        if (!RangeCheck)
        {
            isAiming = false;
            if (moveTo)
                MoveToPoint();
            else
                Chase();
        }
        else
        {
            moveTo = false;
            LookAt();

            if (!isAiming)
                Aiming();

            attackTimer += SH_TimeScaler.deltaTime;
            if (isAiming && attackTimer >= attackCoolTime)
                AttackSign();
        }
    }
    #endregion

    #region Move Methods
    public void SetMovePoint(Transform pos)
    {
        movePointList.Add(pos);
    }
    void MoveToPoint()
    {
        if (movePointList.Count == 0)
            return;

        if (MovePointCheck())
            moveOrder++;

        if (movePointList.Count == moveOrder)
        {
            moveTo = false;
            return;
        }

        moveTo = true;
        animator.SetBool("Run", true);
        attackTimer = 0f;
        agent.speed = moveSpeed * SH_TimeScaler.TimeScale;

        SetDestination(movePointList[moveOrder].position);
    }

    //현재 moveOrder 위치에 근접하게 도달했는지.
    bool MovePointCheck()
    {
        if (Vector3.Distance(gameObject.transform.position, movePointList[moveOrder].position) < 0.5f)
        {
            return true;
        }
        return false;
    }

    //플레이어를 추격한다.
    void Chase()
    {
        animator.SetBool("Run", true);
        attackTimer = 0f;
        agent.speed = moveSpeed * SH_TimeScaler.TimeScale;
        SetDestination(target.transform.position);
    }

    void SetDestination(Vector3 pos)
    {
        agent.SetDestination(pos);
    }

    #endregion

    #region Action Methods
    public void Spawn()
    {
        if (movePointList.Count != 0)
        {
            moveTo = true;
            moveOrder = 0;
        }
        gameObject.SetActive(true);
        animator.SetLayerWeight(1, 1f);
    }

    void LookAt()
    {
        var dir = target.transform.position - transform.position;
        gameObject.transform.rotation = 
            Quaternion.Lerp(gameObject.transform.rotation, Quaternion.LookRotation(dir), rotateSpeed * SH_TimeScaler.deltaTime);
    }
    public void Hit()
    {
        hitSound.Play();

        if (isDead)
            return;

        gameObject.GetComponent<Collider>().enabled = false;
        isDead = true;
        agent.isStopped = true;
        animator.SetLayerWeight(1, 0f);

        switch (Random.Range(0, 2))
        {
            case 0:
                animator.SetTrigger("Die");
                break;
            case 1:
                animator.SetTrigger("Die_02");
                break;
        }
    }
    void Die()
    {
        Destroy(gameObject, 2f);
    }

    //플레이어를 조준한다
    void Aiming()
    {
        var r = Random.Range(0, 2);
        animator.SetBool("Run", false);
        if (r == 0)
            animator.SetTrigger("Shoot_01");
        else
            animator.SetTrigger("Shoot_02");

        agent.speed = 0f;
        isAiming = true;
    }

    //애니메이션 이벤트 함수
    void Aim()
    {
        animator.SetFloat("ShootSpeed", 0f);
    }

    //플레이어를 향해 총알을 발사한다.
    void AttackSign()
    {
        animator.SetFloat("ShootSpeed", 1f);
    }

    //이동 애니메이션 중 발이 땅에 닿는 시점의 프레임 이벤트 함수
    void Foot()
    {
        footSound.Play();
    }

    //공격 모션중 총알이 날아가기 직전 시점의 프레임 이벤트 함수
    void Shoot()
    {
        attackTimer = 0f;
        SH_BulletFactory.Instance.BulletPull(target.GetRandomTargetPart().gameObject, shootPos);
        shootPos.gameObject.GetComponent<AudioSource>().PlayOneShot(SH_SoundContainer.Instance.shotSound);
        isAiming = false;
    }
    //플레이어가 공격 범위 내에 들었는지 체크한다
    bool RangeCheck
        => Vector3.Distance(target.transform.position, transform.position) <= attackRange ? true : false;
    #endregion
}
