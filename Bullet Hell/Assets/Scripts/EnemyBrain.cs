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
            Locating();
            Targeting();
            Shove();
        }
    }



    void Shove() {
        float distanceToTarget = (this.transform.position - target.transform.position).magnitude;
        if (isRanged)
        {
            rb.AddForce(targetPos.normalized * speedMov * distanceToTarget / 10);
        } else
        {
            rb.linearVelocity = targetPos.normalized * speedMov;
        }
        //rb.AddForce(targetPos.normalized * speedMov / distanceToTarget);
        // maybe for an orbiter?

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

        Vector3 objectPos = transform.position;
        targetPos.x = targetPos.x - objectPos.x;
        targetPos.y = targetPos.y - objectPos.y;

        targetAngle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg - 90f - currentAngle;
        currentAngle = currentAngle + (targetAngle) / speedRot;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentAngle));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerLaser"))
        {

            transform.position = new Vector3(
                transform.position.x + (collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity.normalized * collision.gameObject.GetComponent<LaserStats>().Knockback).x,
                transform.position.y + (collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity.normalized * collision.gameObject.GetComponent<LaserStats>().Knockback).y,
                transform.position.x);
            hp -= collision.gameObject.GetComponent<LaserStats>().Damage;
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile") || collision.CompareTag("PlayerLaser")) {
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
                transform.position = new Vector3(
                    transform.position.x + (gameObject.GetComponent<Rigidbody2D>().linearVelocity.normalized * collision.GetComponent<LaserStats>().Knockback * -1).x,
                    transform.position.y + (gameObject.GetComponent<Rigidbody2D>().linearVelocity.normalized * collision.GetComponent<LaserStats>().Knockback * -1).y,
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
            if (collision.CompareTag("PlayerProjectile"))
            {
                Destroy(collision.gameObject);
            }
            
        }
    }
}
