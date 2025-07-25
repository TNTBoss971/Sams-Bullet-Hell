using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Outside Objects")]
    public GameManagement gameManager;

    [Header("Personal Control Variables")]
    public float speedMov;
    public float speedRot;
    public float targetAngle;
    public float currentAngle;
    public Vector3 targetPos;

    public Rigidbody2D rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // point toward the mouse, and move towards it.
        targetPos = Input.mousePosition;
        targetPos.z = 5.23f;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        targetPos.x = targetPos.x - objectPos.x;
        targetPos.y = targetPos.y - objectPos.y;

        targetAngle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg - currentAngle;

        if (Mathf.Abs(targetAngle) > 180)
        {
            currentAngle = currentAngle * -1;
            targetAngle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg - currentAngle;
        }

        currentAngle = currentAngle + (targetAngle) / speedRot;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentAngle - 90f));
        rb.linearVelocity = new Vector2(targetPos.x / speedMov, targetPos.y / speedMov);
        
        //rb.AddForce(new Vector2(mousePos.x / speedMov, mousePos.y / speedMov));
        //was funny, but didn't work
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("EnemyProjectile")) {
            if (collision.GetComponent<ProjectileStats>().ArmorPierce)
            {
                gameManager.playerHp -= collision.GetComponent<ProjectileStats>().Damage;
            }
            else
            {
                gameManager.playerHp -= collision.GetComponent<ProjectileStats>().Damage / gameManager.playerArmor;
            }
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision.gameObject.GetComponent<EnemyBrain>().damagesOnContact) {
            gameManager.playerHp -= collision.gameObject.GetComponent<EnemyBrain>().damage / gameManager.playerArmor;
        }
    }
}
