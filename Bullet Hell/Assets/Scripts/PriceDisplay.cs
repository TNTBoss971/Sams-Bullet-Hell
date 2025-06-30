using TMPro;
using UnityEngine;

public class PriceDisplay : MonoBehaviour
{
    public string resource;
    private MachineLogic machine;
    private TMP_Text textMesh;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        machine = transform.parent.transform.parent.GetComponent<MachineLogic>();
        textMesh = gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        string StrInQ;
        if (resource == "silica")
        {
            StrInQ = machine.silicaPrice.ToString();
            if (StrInQ.Length > 9)
            {
                textMesh.text = StrInQ.Substring(0, 9);
            }
            else
            {
                textMesh.text = StrInQ;
            }
        }
        if (resource == "copper")
        {
            StrInQ = machine.copperPrice.ToString();
            if (StrInQ.Length > 4)
            {
                textMesh.text = StrInQ.Substring(0, 4);
            }
            else
            {
                textMesh.text = StrInQ;
            }
        }
    }
}
