using Unity.VisualScripting;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    [Header("Public Management")]
    public string gameState;

    [Header("Wave Management")]
    public float difficulty = 1;
    public int wave = 0;

    [Header("Outside Objects")]
    //DOWNTIME
    public GameObject downtimeCanvas;
    //WAVE
    public GameObject waveCanvas;
    public GameObject player;
    public GameObject walls;
    public GameObject enemies;

    private GameObject playerHpMeter;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //playerHpMeter = waveCanvas.GetComponentInChildren
        StartWave();
    }

    // Update is called once per frame
    void Update()
    {

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
