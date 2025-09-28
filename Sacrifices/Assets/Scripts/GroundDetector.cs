using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public bool isGrounded;
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {

    }
    
    public void OnTriggerStay2D(Collider2D other)
        {
            isGrounded = true;
        }

    public void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }
}
