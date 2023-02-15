using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private bool conditionState = false;
    private int firstInterrupt = 3, shotNumberIndex;
    public int bossHealth;
    [SerializeField]
    private int[] numberOfShots;
    public static int boss1BulletPower;
    [SerializeField]
    private float mulptipleShotInterrupt, multipleBombsInterrupt;
    private float timeHolder;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private AudioClip fx;
    private AudioSource audioSource;
    [SerializeField]
    private GameObject explosionAnim, coin;
    [SerializeField]
    private GameObject[] bullets;
    private void Start()
    {
        timeHolder = Time.time;
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        BossHealthController.bossHealthControllerInstance.gameObject.SetActive(true);
        BossHealthController.bossHealthControllerInstance.SetValues(bossHealth);
    }
    private void Update()
    {
        IfTimeToShoot();
    }
    private void IfTimeToShoot()
    {
        if (conditionState)
        {
            MultipleBombs();
        }
        else if (!conditionState)
        {
            MultipleShots();
        }
    } 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            bossHealth -= boss1BulletPower;
            StartCoroutine(DamageColorChangeHandler());
            BossHealthController.bossHealthControllerInstance.UseValue(bossHealth);
            if (bossHealth <= 0)
            {
                BossDeathReaction();
            }
        }
    }
    private void MultipleShots()
    {
        if (Time.time > timeHolder + mulptipleShotInterrupt + firstInterrupt && Time.timeScale != 0)
        {
            firstInterrupt = 0;
            Instantiate(bullets[0], new Vector2(transform.position.x, transform.position.y - 0.3f), Quaternion.identity);
            audioSource.PlayOneShot(fx);
            audioSource.volume = PauseMenuController.fxSoundVar;
            if (shotNumberIndex == numberOfShots[0]) { shotNumberIndex = 0; conditionState = true; firstInterrupt = 3; }
            timeHolder = Time.time;
            shotNumberIndex++;
        }
    }
    private void MultipleBombs()
    {
        if (Time.time > timeHolder + multipleBombsInterrupt && Time.timeScale != 0)
        {
            firstInterrupt = 0;
            Instantiate(bullets[1], new Vector2(transform.position.x, transform.position.y - 0.3f), Quaternion.identity);
            if (shotNumberIndex == numberOfShots[1]) { shotNumberIndex = 0; conditionState = false; firstInterrupt = 3; }
            timeHolder = Time.time;
            shotNumberIndex++;
        }
    }
    private IEnumerator DamageColorChangeHandler()
    {
        spriteRenderer.color = new Color(1, 0.2f, 0.2f, 1);
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
    private void BossDeathReaction()
    {
        Destroy(BossHealthController.bossHealthControllerInstance.gameObject);
        LevelManager.enemiesNumberIndex = 0;
        Instantiate(explosionAnim, transform.position, Quaternion.identity);
        Instantiate(coin, transform.position, Quaternion.identity);
        LevelManager.timeIndex++;
        EnemyGroupAwake.enemyGroupCounter++;
        EnemyGroupAwake.bossNumbersHolder--;
        LevelManager.lastTime = Time.time;
        LevelManager.levelManagerInstance.enabled = true;
        Destroy(gameObject);
    }
}
