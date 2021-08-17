using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Stage ���� ������
//1. SH_Stage Ŭ���� �о �������� �ε��ϱ�
//2. ���� �������� �ҷ�����
//3. �������� Ŭ�����ߴ��� üũ�ϱ�
//4. �÷��̾� ����ߴ��� üũ�ϱ�.
public class SH_StageManager : MonoBehaviour
{
    public bool infinityMode;   //�� ���ѽ��� ���
    public bool gameClear;  
    public List<SH_Stage> stageList;
    public SH_Stage currentStage;
    public SH_Panel_GameClear gameClearUI;
    public SH_Panel_StageClear stageClearUI;
    public SH_Panel_GameOver gameOverUI;
    public SH_UI_PauseMenu pauseMenu;
    public SH_Player player;

    public List<Transform> spawnPointList = new List<Transform>();
    public GameObject enemyPrefab;
    public bool inputWaiting;
    private void Start()
    {
        SH_TimeScaler.ReplayTime();
        currentStage = stageList[0];
        inputWaiting = false;
        spawnPointList.Clear();
        foreach(var s in stageList)
        {
            spawnPointList.AddRange(s.spawnPoints);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inputWaiting = true;
            pauseMenu.gameObject.SetActive(true);
            return;
        }

        if (inputWaiting)
        {
            return;
        }

        if (player.isDead)
        {
            GameOver();
        }

        if (gameClear && infinityMode)
        {
            InfinityStage();
            return;
        }


        if (currentStage.stageClear)
        {
            if (currentStage.stageNumber == stageList.Count)
                GameClear();
            else
                NextStage();
        }
    }

    public float infinitySpawnTimer = 4f;
    float timer = 0f;
    void InfinityStage()
    {
        timer += SH_TimeScaler.deltaTime;
        if (infinitySpawnTimer > timer)
            return;

        timer = 0f;
        int spawnCount = Random.Range(1, 6);

        for (int i = 0; i < spawnCount; ++i)
        {
            var randomPos = spawnPointList[Random.Range(0, spawnPointList.Count)];
            var enemy = Instantiate(enemyPrefab, randomPos.position, Quaternion.identity, transform);
        }
    }

    //1. �̹� �������� �ѹ��� +1
    //2. �������� ����Ʈ�� ī��Ʈ�� ���ٸ� ������ ����������� �ǹ�
    //3. ������ ����������� ���� Ŭ���� UI Ȱ��ȭ
    //4. �ƴ϶�� ���� �������� �ε�
    void NextStage()
    {
        currentStage.gameObject.SetActive(false);
        currentStage = stageList[currentStage.stageNumber];
        StageClear();
        currentStage.gameObject.SetActive(true);
    }

    //�̹� ���������� �ٽ� �ε��Ѵ�.
    public void CurrentStageReloading()
    {
        iTween.Stop(Camera.main.gameObject);

        player.transform.position = currentStage.stageStartPos;
        player.transform.rotation = currentStage.stageStartRotate;
        player.Init();

        currentStage.gameObject.SetActive(false);
        currentStage.gameObject.SetActive(true);

        SH_BulletFactory.Instance.ResetPool();
        //Debug.Log("Player Respawn Pos : " + currentStage.stageStartPos);
        //Debug.Log("Player Respawn Rot : " + currentStage.stageStartRotate);
    }

    void GameOver()
    {
        inputWaiting = true;
        gameOverUI.gameObject.SetActive(true);
    }

    void StageClear()
    {
        inputWaiting = true;
        stageClearUI.gameObject.SetActive(true);
    }

    void GameClear()
    {
        gameClear = true;
        inputWaiting = true;
        MouseFree();
        gameClearUI.gameObject.SetActive(true);
    }

    public void InfiniteMode()
    {
        SH_TimeScaler.ReplayTime();
        inputWaiting = false;
        infinityMode = true;
        MouseLock();
        gameClearUI.gameObject.SetActive(false);
    }

    public void Home()
    {
        SH_TimeScaler.ReplayTime();
        MouseFree();
        SceneManager.LoadScene("StartScene");
    }

    public void MouseFree()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void MouseLock()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
