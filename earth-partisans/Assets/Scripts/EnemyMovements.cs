using UnityEngine;

public class EnemyMovements : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(rb.velocity.x, speed * Time.deltaTime);
        Destroy(gameObject,6);
    }
}
