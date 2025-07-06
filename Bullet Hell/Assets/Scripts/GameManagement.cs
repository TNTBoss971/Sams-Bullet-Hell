using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.ShaderGraph.Internal;

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
    public float silicaSalvaged;
    public float copperSalvaged;

    [Header("Downtime Management")]
    public string tab = "player";
    public GameObject playerTab;
    public GameObject machinesTab;
    public GameObject missionTab;
    public GameObject weaponStorage;
    public GameObject moduleStorage;
    public GameObject commitedModuleStorage;
    public GameObject selectedStorageObject;
    public string selectedStorageObjectType;

    [Header("Summary Management")]

    [Header("Player Management")]
    public float playerHp; // health
    public float playerMaxHp; // max health
    public float playerArmor; // any damage taken is divided by the armor value [THIS SHOULD NEVER BE ZERO]
    public float playerHeal; // how much the player heals every second [NOT SURE IF IM GOING TO ALLOW THE PLAYER TO MAKE THIS LESS THEN ZERO]
    private float healTime;
    public float silica; // currency
    public float copper; // currency

    public GameObject[] weaponsEquipped;
    public GameObject[] weaponsSlots;
    public GameObject[] weaponsSlotsWave;
    public List<GameObject> weaponsStored;

    public GameObject[] modulesEquipped;
    public GameObject[] modulesSlots;
    public List<GameObject> modulesStored;
    public List<GameObject> modulesCommited;


    [Header("Outside Objects")]
    //DOWNTIME
    public GameObject downtimeCanvas;
    //WAVE
    public GameObject waveCanvas;
    public GameObject player;
    public GameObject walls;
    public GameObject enemies;
    public MeterLogic playerHpMeter;
    public GameObject bullets;
    //SUMMARY
    public GameObject summaryCanvas;

    [Header("Debug")]
    public string startingState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //playerHpMeter = waveCanvas.GetComponentsInChildren<MeterLogic>()[0];
        if (startingState == "wave")
        {
            StartWave();
        }
        else if (startingState == "downtime")
        {
            StartDowntime();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == "wave")
        {
            playerHpMeter.current = playerHp;
            if (playerHp <= 0)
            {
                SceneManager.LoadScene("Main Menu");
            }

            if (Time.time > healTime)
            {
                healTime = Time.time + 1f;
                playerHp += playerHeal;
                if (playerHp > playerMaxHp)
                {
                    playerHp = playerMaxHp;
                } 
            }

            if (enemies.transform.childCount == 0)
            {
                StartSummary();
            }
        }
    }


    public void StartMainMenu()
    {

    }
    // currently, summary just starts downtime, but in the future, it will give the player stats on how the wave went
    public void StartSummary()
    {
        gameState = "summary";
        waveCanvas.SetActive(false);
        downtimeCanvas.SetActive(false);
        summaryCanvas.SetActive(true);

        player.SetActive(false);
        walls.SetActive(false);
        enemies.SetActive(false);


        silica += silicaSalvaged;
        copper += copperSalvaged;

        silicaSalvaged = 0;
        copperSalvaged = 0;
        StartDowntime();
    }
    public void StartWave()
    {
        gameState = "wave";
        wave++;
        difficulty *= 1.5f;
        waveCanvas.SetActive(true);
        downtimeCanvas.SetActive(false);
        summaryCanvas.SetActive(false);

        playerHpMeter.max = playerMaxHp;
        player.SetActive(true);
        walls.SetActive(true);
        enemies.SetActive(true);

        //spawn enemies
        WaveData currentWave = waveQueue[0];
        if (wave > waveQueue.Length - 1) // infite mode
        {

            currentWave = waveQueue[0];
            enemyPool = currentWave.enemyPool;

            for (int i = 0; i < enemyPool.Length; i++)
            {
                for (int j = 0; j < Random.Range(0, difficulty); j++)
                {
                    NewEnemy(enemyPool[i]);
                }
            }
        }
        else // scripted waves
        {
            currentWave = waveQueue[wave];
            enemyPool = currentWave.enemyPool;
            enemyCount = currentWave.enemyCount;

            for (int i = 0; i < enemyPool.Length; i++)
            {
                for (int j = 0; j < enemyCount[i]; j++)
                {
                    NewEnemy(enemyPool[i]);
                }
            }
        }
    }

    public void StartDowntime()
    {
        gameState = "downtime";
        waveCanvas.SetActive(false);
        downtimeCanvas.SetActive(true);
        summaryCanvas.SetActive(false);

        player.SetActive(false);
        walls.SetActive(false);
        enemies.SetActive(false);

        tab = "player";

        playerTab.SetActive(true);
        machinesTab.SetActive(false);
        missionTab.SetActive(false);
    }

    public void NewEnemy(GameObject enemyObject)
    {
        GameObject newEnemy = Instantiate(enemyObject);
        newEnemy.transform.SetParent(enemies.transform);
        int positionInt = Random.Range(0, 4); // pick a corner for the enemy to spawn in
        if (positionInt == 0)
        {
            newEnemy.transform.position = new Vector3(9.20f, 5.30f, 0.5f);
        }
        else if (positionInt == 1)
        {
            newEnemy.transform.position = new Vector3(-9.20f, 5.30f, 0.5f);
        }
        else if (positionInt == 2)
        {
            newEnemy.transform.position = new Vector3(9.20f, -5.30f, 0.5f);
        }
        else if (positionInt == 3)
        {
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
            missionTab.SetActive(false);

        }
    }

    public void MachinesMenuClicked()
    {
        if (tab != "machines")
        {
            tab = "machines";
            playerTab.SetActive(false);
            machinesTab.SetActive(true);
            missionTab.SetActive(false);
        }
    }

    public void MissionMenuClicked()
    {
        if (tab != "mission")
        {
            tab = "mission";
            playerTab.SetActive(false);
            machinesTab.SetActive(false);
            missionTab.SetActive(true);
        }

    }
    public void EquipWeaponFirst(GameObject obInQ)
    {
        selectedStorageObject = obInQ;
        selectedStorageObjectType = "weapon";
    }

    // weapon config
    public void EquipWeaponSecond(int slot)
    {
        if (selectedStorageObject != null && selectedStorageObjectType == "weapon")
        {
            weaponsEquipped[slot] = selectedStorageObject;

            selectedStorageObject.transform.SetParent(weaponsSlots[slot].transform);
            selectedStorageObject.GetComponent<Button>().enabled = false;
            weaponsStored.Remove(selectedStorageObject);
            selectedStorageObject.transform.position = weaponsSlots[slot].transform.position;
            GameObject newGun = Instantiate(selectedStorageObject.GetComponent<DisplayGunData>().linkedPrefab);
            newGun.transform.SetParent(weaponsSlotsWave[slot].transform);
            newGun.transform.position = newGun.transform.parent.transform.position;
            selectedStorageObject.GetComponent<DisplayGunData>().linkedObject = newGun;
            selectedStorageObject = null;

        }
    }
    public void UnequipWeapon(int slot)
    {
        // InQ stands for In Question, so this means Object In Question
        if (weaponsEquipped[slot])
        {

            GameObject obInQ = weaponsEquipped[slot];
            Destroy(obInQ.GetComponent<DisplayGunData>().linkedObject);
            obInQ.transform.SetParent(weaponStorage.transform);
            weaponsStored.Add(obInQ);
            obInQ.GetComponent<Button>().enabled = true;
            weaponsEquipped[slot] = null;
        }
    }

    // module config
    public void EquipModuleFirst(GameObject obInQ)
    {
        selectedStorageObject = obInQ;
        selectedStorageObjectType = "module";
    }

    public void EquipModuleSecond(int slot)
    {
        if (selectedStorageObject != null && selectedStorageObjectType == "module")
        {
            modulesEquipped[slot] = selectedStorageObject;

            selectedStorageObject.transform.SetParent(modulesSlots[slot].transform);
            selectedStorageObject.GetComponent<Button>().enabled = false;
            modulesStored.Remove(selectedStorageObject);
            selectedStorageObject.transform.position = modulesSlots[slot].transform.position;
            // shouldn't need this, but you never know
            /*GameObject newModule = Instantiate(selectedStorageObject.GetComponent<DisplayModuleData>().linkedPrefab);
            newModule.transform.SetParent(modulesSlotsWave[slot].transform);
            newModule.transform.position = newModule.transform.parent.transform.position;
            selectedStorageObject.GetComponent<DisplayModuleData>().linkedObject = newModule;*/
            
            //(selectedStorageObject.GetComponent<ModuleData>());
            selectedStorageObject = null;

        }
    }
    public void UnequipModule(int slot)
    {

        if (modulesEquipped[slot])
        {

            GameObject obInQ = modulesEquipped[slot];
            //Destroy(obInQ.GetComponent<DisplayModuleData>().linkedObject);
            obInQ.transform.SetParent(moduleStorage.transform);
            modulesStored.Add(obInQ);
            obInQ.GetComponent<Button>().enabled = true;
            modulesEquipped[slot] = null;

            //UnapplyModule(obInQ.GetComponent<ModuleData>());
        }
    }

    public void CommitModule(int slot)
    {
        if (modulesEquipped[slot])
        {
            GameObject obInQ = modulesEquipped[slot];

            obInQ.transform.SetParent(commitedModuleStorage.transform);
            modulesCommited.Add(modulesEquipped[slot]);
            modulesEquipped[slot] = null;
            ApplyModule(obInQ.GetComponent<ModuleData>());
        }
    }


    public void ApplyModule(ModuleData modInQ)
    {
        playerMaxHp += modInQ.hpChange;
        playerHp += modInQ.hpChange;

        playerArmor += modInQ.armorChange;
        if (playerArmor <= 0)
        {
            // divide by 0 protection
            playerArmor = 0.001f;
        }

        playerHeal += modInQ.healChange;
    }
    public void UnapplyModule(ModuleData modInQ)
    {
        playerMaxHp -= modInQ.hpChange;
        playerHp -= modInQ.hpChange;

        playerArmor -= modInQ.armorChange;
        if (playerArmor <= 0)
        {
            // divide by 0 protection
            playerArmor = 0.001f;
        }

        playerHeal -= modInQ.healChange;
    }
}
