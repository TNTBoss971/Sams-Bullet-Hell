using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    [Header("Public Management")]
    public string gameState;

    [Header("Wave Management")]
    public float difficulty = 1;
    public int wave = 0;
    public GameObject[] enemyPool;
    public float spawnRate;
    private float spawnTime = 0.0f;

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
        StartWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == "wave")
        {
            playerHpMeter.current = playerHp;
            if (playerHp <= 0) {
                SceneManager.LoadScene("Game");
            }

            if (Time.time > spawnTime)
            {
                spawnTime = Time.time + spawnRate;
                // execute block of code here
                GameObject newEnemy = Instantiate(enemyPool[Random.Range(0, enemyPool.Length)]);
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

    }

    public void StartDowntime()
    {
        gameState = "downtime";
        waveCanvas.SetActive(false);
        downtimeCanvas.SetActive(true);

        player.SetActive(false);
        walls.SetActive(false);
        enemies.SetActive(false);
    }
    


}
