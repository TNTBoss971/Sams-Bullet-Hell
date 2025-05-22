using System.Collections.Generic;
using UnityEngine;

public class storagePlacement : MonoBehaviour
{
    public GameManagement gameManager;
    public List<GameObject> storedObjs;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        storedObjs = gameManager.weaponsStored;
        float row = 0;
        float col = 0;

        for (int i = 0; i < storedObjs.Count; i++)
        {
            storedObjs[i].GetComponent<RectTransform>().localPosition = new Vector3(col * 100, row * -100);
            col++;
            if (col == 2)
            {
                col = 0;
                row++;
            }
        }
    }
}
