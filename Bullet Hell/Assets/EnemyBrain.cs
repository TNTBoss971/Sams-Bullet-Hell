using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyBrain : MonoBehaviour
{
    [Header("Outside Objects")]
    public GameManagement gameManager;
    public GameObject player;

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
            Targeting();
            Shove();
        }
    }



    void Shove() {
        rb.AddForce(new Vector2 (targetPos.x / speedMov, targetPos.y / speedMov));
    }
    void Targeting()
    {
        targetPos = player.transform.position;
        targetPos.z = 5.23f;

        Vector3 objectPos = transform.position;
        targetPos.x = targetPos.x - objectPos.x;
        targetPos.y = targetPos.y - objectPos.y;

        targetAngle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg - 90f - currentAngle;
        currentAngle = currentAngle + (targetAngle) / speedRot;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentAngle));
    }
}
