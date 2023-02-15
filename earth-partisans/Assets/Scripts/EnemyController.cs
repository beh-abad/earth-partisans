using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool isActive = false;
    [SerializeField]
    private bool hasOneShot, hasMultipleShots, canGoToTarget;
    [SerializeField]
    private int numberOfShots, enemyHealth, minRandomColor, maxRandomColor;
    private int shotNumberIndex;
    [SerializeField]
    private float mulptipleShotInterrupt, minChance, maxChance, chanceBeatTimeMin, chanceBeatTimeMax;//Chance of shooting, ", chance of time for shooting chance, "
    public static int bulletPower2;
    private float canScoreBeBorn, canPowerUpBeBorn, chance, timeHolder;
    public static Vector2[] bulletDirection;
    public static Vector3[] point;
    [SerializeField]
    private Transform holderPos;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite[] enemyColor;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip fx;
    [SerializeField]
    private GameObject explosionAnim, bullet, coin;
    [SerializeField]
    private GameObject[] scoresE;
    public static GameObject[] bullets;
    private EnemyGoesToTarget enemyGoesToTarget;
    private void Start()
    {
        timeHolder = Time.time;
        canScoreBeBorn = Random.Range(-1, 0.7f);
        canScoreBeBorn = Random.Range(-1, 0.7f);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = enemyColor[Random.Range(minRandomColor, maxRandomColor-1)];
        enemyGoesToTarget = GetComponent<EnemyGoesToTarget>();
    }
    private void Update()
    {
        IfTimeToChanceOfShooting();
    }
    private void IfTimeToChanceOfShooting()
    {
        if (Time.time > timeHolder + Random.Range(chanceBeatTimeMin, chanceBeatTimeMax) && !isActive && Time.timeScale != 0)
        {
            audioSource.volume = PauseMenuController.fxSoundVar;
            chance = Random.Range(-10, 10);
            if (chance >= minChance && chance <= maxChance) { isActive = true; }
            timeHolder = Time.time;
        }
        if (hasOneShot && isActive)
        {
            OneShot();
        }
        else if (hasMultipleShots && isActive)
        {
            MultipleShots();
        }
        else if(hasMultipleShots && isActive)
        {
            EnemyGoesToTargetFunc();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            enemyHealth -= bulletPower2;
            StartCoroutine(DamageColorChangeHandler());
            if (enemyHealth <= 0)
            {
                EnemyEndReaction();
            }
        }
    }
    private void OneShot()
    {
        Instantiate(bullet, new Vector2(transform.position.x, transform.position.y - 0.3f), Quaternion.identity);
        audioSource.PlayOneShot(fx);
        isActive = false;
    }
    private void MultipleShots()
    {
        if (Time.time > timeHolder + mulptipleShotInterrupt && Time.timeScale != 0)
        {
            Instantiate(bullet, new Vector2(transform.position.x, transform.position.y - 0.3f), Quaternion.identity);
            audioSource.PlayOneShot(fx);
            if (shotNumberIndex == numberOfShots) { shotNumberIndex = 0; isActive = false; }
            timeHolder = Time.time;
            shotNumberIndex++;
        }
    }
    private void EnemyGoesToTargetFunc()
    {
        enemyGoesToTarget.enabled = true;
    }
    private IEnumerator DamageColorChangeHandler()
    {
        spriteRenderer.color = new Color(1, 0.2f, 0.2f, 1);
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
    private void EnemyEndReaction()
    {
        LevelManager.enemiesNumberIndex--;
        Instantiate(explosionAnim, transform.position, Quaternion.identity);
        if (canScoreBeBorn > 0.6f && canScoreBeBorn < 0.7f) { Instantiate(scoresE[Random.Range(0, scoresE.Length)], transform.position, Quaternion.identity); }
        else if (canPowerUpBeBorn > 0.1f && canPowerUpBeBorn < 0.3f) { Instantiate(scoresE[4], transform.position, Quaternion.identity); }
        Instantiate(coin, transform.position, Quaternion.identity);
        if (LevelManager.enemiesNumberIndex == 0) { LevelManager.timeIndex++; EnemyGroupAwake.enemyGroupCounter++; LevelManager.lastTime = Time.time; LevelManager.levelManagerInstance.enabled = true; }
        Destroy(gameObject);
    }
}
