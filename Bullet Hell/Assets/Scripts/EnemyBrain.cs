using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class EnemyBrain : MonoBehaviour
{
    [Header("Outside Objects")]
    public GameManagement gameManager;
    public Transform target;

    [Header("Enemey Config")]
    public bool isRanged;
    public bool damagesOnContact;
    public float damage;
    public float hp;
    public float silicaValue;
    public float copperValue;
    
    [Header("Personal Control Variables")]
    public float speedRot;
    public float speedMov;
    public float targetAngle;
    public Vector3 targetPos;
    public float currentAngle;
    public Rigidbody2D rb;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 1) == 0) {
            // Finding target
            Locating();
            // Rotating
            Targeting();
            // Moving
            Shove();
        }
    }



    void Shove() {
        float distanceToTarget = (this.transform.position - target.transform.position).magnitude;
        if (isRanged)
        {
            // this add force equation is great for when an enemy doesn't need to make contant with the target.
            rb.AddForce(targetPos.normalized * speedMov * distanceToTarget / 10);
        } else
        {
            // at somepoint, I would like to make the melee enemies also use AddForce, but this works for now
            rb.linearVelocity = targetPos.normalized * speedMov;
        }

    }


    void Locating()
    {
        Vector3 pos = this.transform.position;
        float dist = float.PositiveInfinity;
        TargetablePlayer targ = null;
        // find closest valid target
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

        Vector3 objectPos = transform.position;
        targetPos.x = targetPos.x - objectPos.x;
        targetPos.y = targetPos.y - objectPos.y;

        targetAngle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg - 90f - currentAngle;
        currentAngle = currentAngle + (targetAngle) / speedRot;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentAngle));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // verify that trigger is actually something this can interact with
        if (collision.CompareTag("PlayerProjectile") || collision.CompareTag("PlayerLaser"))
        {
            // projectiles and lasers work slightly different in how they apply knockback
            if (collision.CompareTag("PlayerProjectile"))
            {
                rb.AddForce(collision.GetComponent<Rigidbody2D>().linearVelocity * 10);
                transform.position = new Vector3(
                    transform.position.x + (collision.GetComponent<Rigidbody2D>().linearVelocity.normalized * collision.GetComponent<ProjectileStats>().Knockback).x,
                    transform.position.y + (collision.GetComponent<Rigidbody2D>().linearVelocity.normalized * collision.GetComponent<ProjectileStats>().Knockback).y,
                    transform.position.x);
                hp -= collision.GetComponent<ProjectileStats>().Damage;
            }
            else if (collision.CompareTag("PlayerLaser"))
            {
                rb.AddForce(rb.linearVelocity * 10);
                transform.position = new Vector3(
                    transform.position.x + (-1 * collision.GetComponent<LaserStats>().Knockback * rb.linearVelocity.normalized).x,
                    transform.position.y + (-1 * collision.GetComponent<LaserStats>().Knockback * rb.linearVelocity.normalized).y,
                    transform.position.x);
                hp -= collision.GetComponent<LaserStats>().Damage;
                print(this.name + (hp));
            }
            //kill the enemy
            if (hp <= 0)
            {
                //calculate silica gain
                gameManager.silicaSalvaged += silicaValue + Random.Range(-0.75f, 0.75f);

                //calculate copper gain
                gameManager.copperSalvaged += copperValue + Random.Range(-0.02f, 0.02f);


                //Destroy(this); gives a really cool "corpse" effect, but does some wierd stuff when the enemy has a gun
                //definatlly gonna use later. Possible solution is to find all children marked "attachment" and disable them;
                Destroy(this.gameObject);
                gameManager.enemiesLeft--;
            }
            // at some point, i should probably make it so this checks for pierce, but that doesn't exist yet
            if (collision.CompareTag("PlayerProjectile"))
            {
                Destroy(collision.gameObject);
            }

        }
    }
}
