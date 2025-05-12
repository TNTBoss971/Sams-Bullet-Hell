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

    public Rigidbody2D rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        targetAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg - 90f - currentAngle;
        currentAngle = currentAngle + (targetAngle) / speedRot;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentAngle));
        rb.linearVelocity = new Vector2 (mousePos.x / speedMov, mousePos.y / speedMov);
        //rb.AddForce(new Vector2(mousePos.x / speedMov, mousePos.y / speedMov));
        //was funny, but didn't work
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("EnemyProjectile")) {
            Destroy(collision.gameObject);
        }
    }
}
