using UnityEngine;

public class MeterLogic : MonoBehaviour
{
    public float range;
    public float max;
    public float current;

    private RectTransform rectTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.localScale = new Vector3((current / max), 1, 1);
    }
}
