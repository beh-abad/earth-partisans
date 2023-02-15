using UnityEngine;
using UnityEditor;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private GameObject Explosion;
    [SerializeField]
    private int bulletPower;
    private void Awake()
    {
        BossController.boss1BulletPower = bulletPower;
        EnemyController.bulletPower2 = bulletPower;
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -4);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        MoveBullet();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Destroyer"))
        {
            Destroy(gameObject);
        }
    }
    private void MoveBullet()
    {
        transform.position += new Vector3(0, bulletSpeed * Time.deltaTime, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Asteroid") || collision.gameObject.CompareTag("Boss"))
        {
            BulletReaction();
        }
    }
    private void BulletReaction()
    {
        Instantiate(Explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
