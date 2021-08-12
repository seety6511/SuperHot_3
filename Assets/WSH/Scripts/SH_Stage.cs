using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

//스테이지 정보 파일
//1. 이번 스테이지에 생성되어야 할 적 리스트
//2. 이번 스테이지에서 사용 가능한 AI이동 포인트
//3. 이번 스테이지에서 사용 가능한 스폰 포인트
//4. 이번 스테이지의 적이 전부 처리 되었는지 체크
//5. 스테이지 넘버
public class SH_Stage : MonoBehaviour
{
    [System.Serializable]
    public class SpawnData
    {
        public GameObject prefab;
        public Transform spawnPoint;
        public float spawnTime;
        public float triggerDistance;   //소환을 유도하는 플레이어와의 거리. spawnTime이 0일 경우에만 적용한다.
        public bool isSpawn;    //스폰이 완료되었는지 여부
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
    public Vector3 stageStartPos;   //스테이지 시작시 플레이어가 위치했던 위치
    public Quaternion stageStartRotate;
    public SH_StageManager sm;
    bool posSave;

    private void Start()
    {
        sm = FindObjectOfType<SH_StageManager>();
        posSave = false;
    }

    //오브젝트 활성화시
    //모든 상태를 초기상태로 만든다.
    //에너미 스폰 타이머는 스테이지 오브젝트가 활성화된 시간을 기준으로 상대 측정한다.
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

    //stage의 자식 오브젝트중 SH_Enemy 컴포넌트를 전부 가져온다. 0 이면 몬스터가 전부 사망했다는 의미이다.
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
