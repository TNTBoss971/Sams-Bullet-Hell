using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGunBrain : MonoBehaviour
{
    [Header("Outside Objects")]
    public GameManagement gameManager;

    [Header("Personal Control Variables")]
    public GameObject ammo;
    public PlayerController playerController;
    private Vector3 targetPos;

    //inputs
    InputAction attackAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackAction = InputSystem.actions.FindAction("Attack");
    }

    // Update is called once per frame
    void Update()
    {
        targetPos = playerController.targetPos;
        if (attackAction.WasPressedThisFrame()) {
            Fire();
        }
    }



    void Fire()
    {
        GameObject bullet = Instantiate(ammo);
        bullet.transform.position = this.transform.position;
        bullet.transform.rotation = this.transform.rotation;
        bullet.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(targetPos.x, targetPos.y).normalized * bullet.GetComponent<ProjectileStats>().Speed;
    }
}
