using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    [Header("Public Management")]
    public string gameState;

    [Header("Wave Management")]
    public float difficulty = 1;
    public int enemiesLeft;
    public int wave = 0;
    public GameObject[] enemyPool;
    public WaveData[] waveQueue;
    public int[] enemyCount;

    [Header("Downtime Management")]
    public string tab = "player";
    public GameObject playerTab;
    public GameObject machinesTab;

    [Header("Player Management")]
    public float playerHp;

    [Header("Outside Objects")]
    //DOWNTIME
    public GameObject downtimeCanvas;
    //WAVE
    public GameObject waveCanvas;
    public GameObject player;
    public GameObject walls;
    public GameObject enemies;

    public MeterLogic playerHpMeter;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //playerHpMeter = waveCanvas.GetComponentsInChildren<MeterLogic>()[0];
        //StartWave();
        StartDowntime();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == "wave")
        {
            playerHpMeter.current = playerHp;
            if (playerHp <= 0) {
                SceneManager.LoadScene("Main Menu");
            }

            if (enemiesLeft == 0)
            {
                StartDowntime();
            }
        }
    }


    public void StartMainMenu()
    {

    }

    public void StartWave()
    {
        gameState = "wave";
        wave++;
        waveCanvas.SetActive(true);
        downtimeCanvas.SetActive(false);

        playerHpMeter.max = playerHp;
        player.SetActive(true);
        walls.SetActive(true);
        enemies.SetActive(true);

        //spawn enemies
        enemyPool = waveQueue[wave - 1].enemyPool;
        int[] enemyCount = waveQueue[wave - 1].enemyCount;
        for (int i = 0; i < enemyPool.Length; i++)
        {
            for (int j = 0; j < enemyCount[i]; j++)
            {
                NewEnemy(enemyPool[i]);
            }
        }
    }

    public void StartDowntime()
    {
        gameState = "downtime";
        waveCanvas.SetActive(false);
        downtimeCanvas.SetActive(true);

        player.SetActive(false);
        walls.SetActive(false);
        enemies.SetActive(false);

        playerTab.SetActive(true);
        machinesTab.SetActive(false);
    }
    
    public void NewEnemy(GameObject enemyObject)
    {
        GameObject newEnemy = Instantiate(enemyObject);
        newEnemy.transform.SetParent(enemies.transform);
        int positionInt = Random.Range(0, 4);
        if (positionInt == 0) {
            newEnemy.transform.position = new Vector3(9.20f, 5.30f, 0.5f);
        } else if (positionInt == 1) {
            newEnemy.transform.position = new Vector3(-9.20f, 5.30f, 0.5f);
        } else if (positionInt == 2) {
            newEnemy.transform.position = new Vector3(9.20f, -5.30f, 0.5f);
        } else if (positionInt == 3) {
            newEnemy.transform.position = new Vector3(-9.20f, -5.30f, 0.5f);
        }
        newEnemy.GetComponent<EnemyBrain>().gameManager = this;
        enemiesLeft++;
    }

    public void PlayerMenuClicked()
    {
        if (tab != "player")
        {
            tab = "player";
            playerTab.SetActive(true);
            machinesTab.SetActive(false);

        }
    }

    public void MachinesMenuClicked()
    {
        if (tab != "machines")
        {
            tab = "machines";
            playerTab.SetActive(false);
            machinesTab.SetActive(true);
        }
    }
}
