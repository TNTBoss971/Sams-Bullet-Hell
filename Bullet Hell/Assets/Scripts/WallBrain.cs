using UnityEngine;

public class WallBrain : MonoBehaviour
{
    /*
    Yeah, I gave a wall a brain. 
    What are you gonna do about it? 
    Nothing.
    Because I just gave a wall a brain.
    */

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6) {
            Destroy(collision.gameObject);
        }
    }

}
