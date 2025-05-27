using UnityEngine;

public class MachineLogic : MonoBehaviour
{
    public GameObject[] objectPool;
    public float[] objectOdds;
    public GameObject dispensedObject;

    private GameManagement gameManager;
    public string storageType;
    public GameObject storage;
    public float result;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindObjectsByType<GameManagement>(FindObjectsSortMode.None)[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Dispense()
    {
        result = Random.Range(0.001f, 100);
        float currentOdds = objectOdds[0];
        for (int i = 0; i < objectPool.Length; i++)
        {
            if (currentOdds >= result)
            {
                dispensedObject = objectPool[i];
                GameObject newObject = Instantiate(dispensedObject);
                newObject.transform.SetParent(storage.transform);
                if (storageType == "weapon")
                {
                    gameManager.weaponsStored.Add( newObject);
                }
                return;
            } else
            {
                currentOdds += objectOdds[i];
            }
        }
    }
}
