using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SH_Enemy : MonoBehaviour
{
    #region Variables
    public float moveSpeed; //�̵��ӵ� 
    public float animSpeed = 1f; //�ִϸ��̼� �⺻ �ӵ�
    public float attackRange;   //������ �ϱ� ���� �ʿ��� �ּ� ��Ÿ�
    public float attackCoolTime;    //���� ������ �ϱ� ���� �ð�
    public float attackTimer;   //�׳� ���� Ÿ�̸�
    public float rotateSpeed = 5f;

    public bool isAiming;   //�������϶�
    public bool isDead;
    public bool moveTo;     //���� ��ġ�� �̵����϶�
    public int moveOrder;   //�̵� ��ġ ����. movePointList�� �Բ� ���
    public List<Transform> movePointList = new List<Transform>();   //�̵��ؾ��� ��ġ

    public Transform shootPos;  //�Ѿ��� �߻�Ǵ� ��ġ
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

    //1. ���ݹ����� ������� üũ
    //2. ���ݹ����� ����ٸ� Chase
    //3. ���ݹ��� �ȿ� ���ٸ� MoveToPoint
    //4. MoveToPoint �� ���ݹ��� ���� ���´ٸ� Chase�� ��ȯ
    //5. ��� MovePoint�� ��ȸ�ߴٸ� Chase�� ��ȯ
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

    //���� moveOrder ��ġ�� �����ϰ� �����ߴ���.
    bool MovePointCheck()
    {
        if (Vector3.Distance(gameObject.transform.position, movePointList[moveOrder].position) < 0.5f)
        {
            return true;
        }
        return false;
    }

    //�÷��̾ �߰��Ѵ�.
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

    //�÷��̾ �����Ѵ�
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

    //�ִϸ��̼� �̺�Ʈ �Լ�
    void Aim()
    {
        animator.SetFloat("ShootSpeed", 0f);
    }

    //�÷��̾ ���� �Ѿ��� �߻��Ѵ�.
    void AttackSign()
    {
        animator.SetFloat("ShootSpeed", 1f);
    }

    //�̵� �ִϸ��̼� �� ���� ���� ��� ������ ������ �̺�Ʈ �Լ�
    void Foot()
    {
        footSound.Play();
    }

    //���� ����� �Ѿ��� ���ư��� ���� ������ ������ �̺�Ʈ �Լ�
    void Shoot()
    {
        attackTimer = 0f;
        SH_BulletFactory.Instance.BulletPull(target.GetRandomTargetPart().gameObject, shootPos);
        shootPos.gameObject.GetComponent<AudioSource>().PlayOneShot(SH_SoundContainer.Instance.shotSound);
        isAiming = false;
    }
    //�÷��̾ ���� ���� ���� ������� üũ�Ѵ�
    bool RangeCheck
        => Vector3.Distance(target.transform.position, transform.position) <= attackRange ? true : false;
    #endregion
}
