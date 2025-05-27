using UnityEngine;

public class weaponSwap : MonoBehaviour
{
    private GameManagement gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [System.Obsolete]
    void Start()
    {
        gameManager = FindObjectsOfType<GameManagement>()[0];
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
