using UnityEngine;
using TMPro;

public class ResourceDisplay : MonoBehaviour
{
    public string resource;
    private GameManagement gameManager;
    private TMP_Text textMesh; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindObjectsByType<GameManagement>(FindObjectsSortMode.None)[0];
        textMesh = gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        string StrInQ;
        if (resource == "silica")
        {
            StrInQ = gameManager.silica.ToString();
            if (StrInQ.Length > 9)
            {
                textMesh.text = StrInQ.Substring(0, 9);
            } else
            {
                textMesh.text = StrInQ;
            }
        }
        if (resource == "copper")
        {
            textMesh.text = gameManager.copper.ToString();
        }
    }
}
