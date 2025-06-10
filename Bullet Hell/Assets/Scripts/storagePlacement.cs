using System.Collections.Generic;
using UnityEngine;

public class storagePlacement : MonoBehaviour
{
    private GameManagement gameManager;
    public List<GameObject> storedObjs;
    public bool reversed;
    public string storedKind;
    public float xOffset;
    public float yOffset;
    public float numOfCol;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindObjectsByType<GameManagement>(FindObjectsSortMode.None)[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (storedKind == "weapons")
        {
            storedObjs = gameManager.weaponsStored;
        }
        if (storedKind == "modules")
        {
            storedObjs = gameManager.modulesStored;
        }
        if (storedKind == "commitedModules")
        {
            storedObjs = gameManager.modulesCommited;
        }

        float row = 0;
        float col = 0;


        for (int i = 0; i < storedObjs.Count; i++)
        {
            if (reversed)
            {
                storedObjs[i].GetComponent<RectTransform>().localPosition = new Vector3(col * -100 + xOffset, row * -100 + yOffset);
            }
            else
            {
                storedObjs[i].GetComponent<RectTransform>().localPosition = new Vector3(col * 100 + xOffset, row * -100 + yOffset);
            }
            col++;
            if (col == numOfCol)
            {
                col = 0;
                row++;
            }
        }
    }
}
