using UnityEngine;

public class RbSpeed : MonoBehaviour
{
    [SerializeField]
    private int asteroidSpeed;
    private Rigidbody2D rb;
    public static RbSpeed rbSpeedInstance;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x, asteroidSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Destroyer"))
        {
            Destroy(gameObject);
        }
    }
}
