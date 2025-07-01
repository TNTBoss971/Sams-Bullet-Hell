using UnityEngine;

public class EnemyGunBrain : MonoBehaviour
{
    [Header("Outside Objects")]
    public GameManagement gameManager;
    public Transform target;
    public GameObject player;

    [Header("Personal Control Variables")]
    public float speedRot;
    public float targetAngle;
    public float currentAngle;
    public GameObject ammo;
    private Vector3 targetPos;

    public float range;
    public bool canFire;
    public float fireRate;
    private float fireTime = 0.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        gameManager = FindObjectsByType<GameManagement>(FindObjectsSortMode.None)[0];
    }

    // Update is called once per frame
    void Update()
    {
        Locating();
        Targeting();
        if (Time.time > fireTime && canFire)
        {
            fireTime = Time.time + fireRate;
            // execute block of code here
            Fire();
        }

    }


    void Fire() {
        GameObject bullet = Instantiate(ammo);
        bullet.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);
        bullet.transform.SetParent(gameManager.bullets.transform, true);
        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(targetPos.x, targetPos.y).normalized * bullet.GetComponent<ProjectileStats>().Speed);
    }

    void Locating()
    {
        Vector3 pos = this.transform.position;
        float dist = float.PositiveInfinity;
        TargetablePlayer targ = null;
        foreach (var obj in TargetablePlayer.Entities)
        {
            var d = (pos - obj.transform.position).sqrMagnitude;
            if (d < dist)
            {
                targ = obj;
                dist = d;
            }
        }
        target = targ.transform;
    }

    void Targeting()
    {
        targetPos = target.position;
        targetPos.z = 5.23f;

        Vector3 objectPos = this.transform.position;
        targetPos.x = targetPos.x - objectPos.x;
        targetPos.y = targetPos.y - objectPos.y;

        if (targetPos.sqrMagnitude <= range * range) { 
            canFire = true;
        } else { 
            canFire = false;
        }

        targetAngle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg - 90f - currentAngle;
        currentAngle = currentAngle + (targetAngle) / speedRot;
        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentAngle));
    }

    
}
