using UnityEngine;

public class BossData : MonoBehaviour
{
    public GameObject player;
    public float hp;
    public float maxHp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
