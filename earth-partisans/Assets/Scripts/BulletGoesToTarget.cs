using UnityEngine;

public class test : MonoBehaviour
{  
    private bool hasInitialized = false;
    private GameObject target;
    [SerializeField]
    private GameObject Explosion;
    private Rigidbody2D rb;
    private Vector2 goToTarget;
    private const int r = 4;
    [SerializeField]
    private Sprite[] image;
    private SpriteRenderer imageRenderer;   
    [SerializeField]
    private int bulletPower;
    // Start is called before the first frame update
    void Start()
    {
        imageRenderer = GetComponent<SpriteRenderer>();
        imageRenderer.sprite = image[Random.Range(0, 5)];
        CharacterController1.enemyBulletPower = bulletPower;
    }

    private void Update()
    {
        if (rb == null && hasInitialized == false) { rb = GetComponent<Rigidbody2D>(); }
        if (hasInitialized == false)
        {
            target = FindObjectOfType<CharacterController>().gameObject;
            rb = GetComponent<Rigidbody2D>();
            goToTarget = (target.transform.position - transform.position).normalized * r;
            rb.velocity = new Vector2(goToTarget.x, goToTarget.y);
            hasInitialized = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Destroyer"))
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("SpaceShip"))
        {
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bubble"))
        {
            Destroy(collision.gameObject);
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    } 
}
