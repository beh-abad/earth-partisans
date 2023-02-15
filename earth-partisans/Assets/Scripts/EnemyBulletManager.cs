using System.Collections;
using UnityEngine;
using System;
public class EnemyBulletManager : MonoBehaviour
{  
    [SerializeField]
    private bool canBulletGoStraight, canBulletGoToTarget, canBulletFallOff;
    [SerializeField]
    private int minRandomSpeed, maxRandomSpeed, minRandomColor, maxRandomColor, bulletPower;
    private int enemyBulletSpeed;
    [SerializeField]
    private float force, repeatBeat, explosionRadius;
    private Vector2 goToTarget;
    [SerializeField]
    private Sprite[] image;
    private SpriteRenderer imageRenderer;
    private Rigidbody2D rb;
    [SerializeField]
    private GameObject explosion;
    private GameObject target;
    [SerializeField]
    private LayerMask forceMaker;
    void Start()
    {
        enemyBulletSpeed = UnityEngine.Random.Range(minRandomSpeed, maxRandomSpeed);
        imageRenderer = GetComponent<SpriteRenderer>();
        imageRenderer.sprite = image[UnityEngine.Random.Range(minRandomColor, maxRandomColor)];
        CharacterController1.enemyBulletPower = bulletPower;
        rb = GetComponent<Rigidbody2D>();
        try
        {
            target = GameObject.FindGameObjectWithTag("SpaceShip").gameObject;
        }
        catch (NullReferenceException)
        {
            Destroy(gameObject);
        }
        if (canBulletGoToTarget)
        {
            BulletGoesToTarget();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (canBulletGoStraight)
        {
            BulletGoesStraight();
        }
        else if (canBulletFallOff)
        {
            BulletFallsOff();
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
            BulletReaction();
        }
    }
    private void BulletGoesStraight()
    {
        rb.velocity = new Vector2(rb.velocity.x, enemyBulletSpeed * Time.deltaTime);
    }
    private void BulletGoesToTarget()
    {
        try
        {
            goToTarget = (target.transform.position - transform.position).normalized * enemyBulletSpeed;
            rb.velocity = new Vector2(goToTarget.x, goToTarget.y);
        }
        catch (NullReferenceException)
        {
            Destroy(gameObject);
        }
    }
    private void BulletFallsOff()
    {
        try
        {
            if (transform.position.x >= target.transform.position.x - 0.7f && transform.position.x <= target.transform.position.x + 0.7f)
            {
                transform.parent = null;
                rb.isKinematic = false;
            }
        }
        catch (MissingReferenceException)
        {
            enabled = false;
        }
    }
    private void ExplosionForce()
    {
        if (transform.position.y - UnityEngine.Random.Range(0, 2) <= target.transform.position.y)
        {
            Collider2D spaceship = Physics2D.OverlapCircle(transform.position, explosionRadius, forceMaker);
            Vector2 direction = (target.transform.position - transform.position).normalized;
            spaceship.GetComponent<Rigidbody2D>().AddForce(direction * force);
            BulletReaction();
        }
    }
    private void BulletReaction()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
