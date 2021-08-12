using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

//�������� ���� ����
//1. �̹� ���������� �����Ǿ�� �� �� ����Ʈ
//2. �̹� ������������ ��� ������ AI�̵� ����Ʈ
//3. �̹� ������������ ��� ������ ���� ����Ʈ
//4. �̹� ���������� ���� ���� ó�� �Ǿ����� üũ
//5. �������� �ѹ�
public class SH_Stage : MonoBehaviour
{
    [System.Serializable]
    public class SpawnData
    {
        public GameObject prefab;
        public Transform spawnPoint;
        public float spawnTime;
        public float triggerDistance;   //��ȯ�� �����ϴ� �÷��̾���� �Ÿ�. spawnTime�� 0�� ��쿡�� �����Ѵ�.
        public bool isSpawn;    //������ �Ϸ�Ǿ����� ����
    }
    public int stageNumber;

    public List<SpawnData> spawnDatas = new List<SpawnData>();
    public List<Transform> movePoints = new List<Transform>();
    public List<Transform> spawnPoints = new List<Transform>();
    public Transform enemyContainer;

    public bool allSpawn;
    public bool stageClear;

    public float spawnTimer;
    public int currentSpawnDataIndex;
    public SH_Player player;
    public Vector3 stageStartPos;   //�������� ���۽� �÷��̾ ��ġ�ߴ� ��ġ
    public Quaternion stageStartRotate;
    public SH_StageManager sm;
    bool posSave;

    private void Start()
    {
        sm = FindObjectOfType<SH_StageManager>();
        posSave = false;
    }

    //������Ʈ Ȱ��ȭ��
    //��� ���¸� �ʱ���·� �����.
    //���ʹ� ���� Ÿ�̸Ӵ� �������� ������Ʈ�� Ȱ��ȭ�� �ð��� �������� ��� �����Ѵ�.
    void OnEnable()
    {
        player = FindObjectOfType<SH_Player>();

        allSpawn = false;
        stageClear = false;
        currentSpawnDataIndex = 0;
        spawnTimer = 0f;
        foreach (var s in spawnDatas)
        {
            s.isSpawn = false;
        }

        var enemys = enemyContainer.GetComponentsInChildren<SH_Enemy>();
        foreach (var e in enemys)
        {
            Destroy(e.gameObject);
        }

        if (!posSave)
        {
            stageStartPos = player.transform.position;
            stageStartRotate = player.transform.rotation;
            posSave = true;
        }

        Debug.Log("Save Pos : " + stageStartPos);
        Debug.Log("Save Rot : " + stageStartRotate);
    }

    private void Update()
    {
        if (sm.inputWaiting)
            return;
        spawnTimer += SH_TimeScaler.deltaTime;
        SpawnEnemy();
        ClearCheck();
    }

    void SpawnEnemy()
    {
        if (currentSpawnDataIndex == spawnDatas.Count())
        {
            allSpawn = true;
            return;
        }

        var targetEnemys = spawnDatas.Where(s => !s.isSpawn && s.spawnTime <= spawnTimer);

        if (targetEnemys.Count() == 0)
            return;
        foreach(var t in targetEnemys)
        {
            if (t.spawnTime == 0)
            {
                if(Vector3.Distance(player.gameObject.transform.position, t.spawnPoint.position) < t.triggerDistance)
                {
                    var e = Instantiate(t.prefab, t.spawnPoint.position, Quaternion.identity, enemyContainer);
                    e.name = "Enemy_" + currentSpawnDataIndex;
                    currentSpawnDataIndex++;
                    t.isSpawn = true;
                }
            }
            else
            {
                var e = Instantiate(t.prefab, t.spawnPoint.position, Quaternion.identity, enemyContainer);
                e.name = "Enemy_" + currentSpawnDataIndex;
                currentSpawnDataIndex++;
                t.isSpawn = true;
            }
        }
    }

    //stage�� �ڽ� ������Ʈ�� SH_Enemy ������Ʈ�� ���� �����´�. 0 �̸� ���Ͱ� ���� ����ߴٴ� �ǹ��̴�.
    void ClearCheck()
    {
        if (!allSpawn)
            return;

        var aliveEnemys = enemyContainer.GetComponentsInChildren<SH_Enemy>();
        
        if (aliveEnemys.Count() > 0)
            return;

        stageClear = true;
    }
}
