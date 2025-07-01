using UnityEngine;

public class GunBrain : MonoBehaviour
{
    [Header("Outside Objects")]
    private GameManagement gameManager;
    public Transform target;
    public Vector3 lastTargetPosition;
    private GameObject player;
    private GameObject boundBullet;

    [Header("Personal Control Variables")]
    public float speedRot;
    public float targetAngle;
    public float currentAngle;
    public GameObject ammo;
    private Vector3 targetPos;

    public bool canChangeTarget = true;
    public float range;
    public bool canFire;
    public float fireRate;
    private float fireTime = 0.0f;

    [Header("Special Characteristics")]
    public int shots;
    public bool isMinigun;
    public bool isMaxigun;
    public bool isLaser;
    public bool isShotgun;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindObjectsByType<GameManagement>(FindObjectsSortMode.None)[0];
        player = FindObjectsByType<PlayerController>(FindObjectsSortMode.None)[0].gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Locating();
        Targeting();
        if (Time.time > fireTime && canFire)
        {
            fireTime = Time.time + fireRate;
            // gun go THONK
            Fire();
            if (isMinigun)
            {
                canChangeTarget = false;
                for (int i = 0; i < shots - 1; i++)
                {
                    Invoke(nameof(Fire), i / 5f);
                }
                Invoke(nameof(AllowForTargeting), shots / 5f);
            }

        }
        if (boundBullet != null)
        {
            if (isLaser)
            {
                if (target != null)
                {
                    lastTargetPosition = target.position;

                }
                canChangeTarget = false;
                boundBullet.GetComponent<LaserStats>().startingPosition = transform.position;
                boundBullet.GetComponent<LaserStats>().endingPosition = lastTargetPosition; //(transform.position - lastTargetPosition).magnitude * transform.right;
            }
        }
        else
        {
            if (isLaser)
            {
                canChangeTarget = true;
            }
        }

    }
    void Fire()
    {
        
        if (target != null)
        {
            
            GameObject bullet = Instantiate(ammo);
            if (isLaser)
            {
                //bullet.transform.position = this.transform.position;
                bullet.transform.SetParent(gameManager.bullets.transform, true);
                bullet.GetComponent<LaserStats>().startingPosition = transform.position;
                bullet.GetComponent<LaserStats>().endingPosition = (transform.position - target.position).magnitude * transform.right;
                boundBullet = bullet;
            }
            else
            {
                bullet.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);
                bullet.transform.SetParent(gameManager.bullets.transform, true);
                bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(targetPos.x, targetPos.y).normalized * bullet.GetComponent<ProjectileStats>().Speed);
            }
        }
    }
    void Locating() {
        Vector3 pos = this.transform.position;
        float dist = float.PositiveInfinity;
        Targetable targ = null;
        foreach (var obj in Targetable.Entities) {
            var d = (pos - obj.transform.position).sqrMagnitude;
            if (d < dist) {
                targ = obj;
                dist = d;
            }
        }
        if (targ != null && canChangeTarget) {
            target = targ.transform;
        }
    }

    void Targeting() {
        if (target != null) {
            targetPos = target.position;
            targetPos.z = 5.23f;

            Vector3 objectPos = this.transform.position;
            targetPos.x = targetPos.x - objectPos.x;
            targetPos.y = targetPos.y - objectPos.y;

            if (targetPos.sqrMagnitude <= range * range)
            {
                canFire = true;
            }
            else
            {
                canFire = false;
            }

            targetAngle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg - 90f - currentAngle;
            currentAngle = currentAngle + (targetAngle) / speedRot;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentAngle));
        }
    }
    void AllowForTargeting()
    {
        canChangeTarget = true;
    }
}
