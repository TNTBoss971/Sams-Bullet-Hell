using UnityEngine;

public class weaponSwap : MonoBehaviour
{
    private GameManagement gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        gameManager = FindObjectsByType<GameManagement>(FindObjectsSortMode.None)[0];
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Clicked()
    {
        gameManager.EquipWeaponFirst(this.gameObject);
    }
}
