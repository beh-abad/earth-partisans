using UnityEngine.UI;
using UnityEngine;
using System;


public class CharacterController1 : MonoBehaviour
{
    public static bool isDead = false;
    private int bulletsIndex = 0;
    public static int scoreValue;
    private float lastShot, spaceshipSizeX, spaceshipSizeY;
    private readonly float beginShot = 1.5f;
    private float health, armor;
    private float healthValue, armorValue, healthHolder, armorHolder;
    private Text healthText, armorText, scoreText;
    public static int enemyBulletPower;
    private Rigidbody2D rb;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip audioClip1, audioClip2;
    private GameObject canvas, text1, text2, text3;
    [SerializeField]
    private GameObject explosionAnim;
    [SerializeField]
    private GameObject[] bulletsC;
    public static CharacterController1 CharacterControllerInstance;
    private Vector2 PositionState;
    private void Awake()
    {
        PositionState = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }
    void Start()
    {
        health = 6;
        armor = 6;
        healthValue = 100;
        armorValue = 100;
        bulletsIndex = PlayerPrefs.GetInt("lastChoose"); ;
        audioSource.volume = PauseMenuController.fxSoundVar;
        canvas = GameObject.Find("Canvas0");
        rb = GetComponent<Rigidbody2D>();
        spaceshipSizeX = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        spaceshipSizeY = GetComponent<SpriteRenderer>().bounds.size.y / 2;
        text1 = canvas.transform.GetChild(0).transform.GetChild(1).gameObject;
        text2 = canvas.transform.GetChild(0).transform.GetChild(3).gameObject;
        text3 = canvas.transform.GetChild(0).transform.GetChild(4).transform.GetChild(0).gameObject;
        healthText = text1.GetComponent<Text>();
        armorText = text2.GetComponent<Text>();
        scoreText = text3.GetComponent<Text>();
        healthText.text = Convert.ToString(healthValue);
        armorText.text = Convert.ToString(armorValue);
        CharacterControllerInstance = this;
    }
    void Update()
    {
        InputAndCheckPositionFunction();
    }
    private void InputAndCheckPositionFunction()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, (PositionState.x * -1) + spaceshipSizeX, PositionState.x - spaceshipSizeX), transform.position.y);
        transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, (PositionState.y * -1) + spaceshipSizeY, PositionState.y - spaceshipSizeY));
        float deltaVar = Time.time * Time.deltaTime;
        if (deltaVar >= beginShot * Time.deltaTime && deltaVar >= (lastShot + 0.17f) * Time.deltaTime && Time.timeScale != 0)
        {
            Instantiate(bulletsC[bulletsIndex], new Vector2(transform.position.x, transform.position.y + 0.52f), Quaternion.identity);
            lastShot = Time.time;
        }
#if UNITY_ANDROID
        if (Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            rb.velocity = new Vector2(0, 0);
            Vector2 position = new Vector2(-Input.GetTouch(0).deltaPosition.y, Input.GetTouch(0).deltaPosition.x);
            transform.Translate(position * 0.5f * Time.deltaTime);
        }
#endif
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        healthHolder = Convert.ToSingle((Math.Ceiling(healthValue / health)) * enemyBulletPower);
        armorHolder = Convert.ToSingle((Math.Ceiling(armorValue / armor)) * enemyBulletPower);
        audioSource.volume = PauseMenuController.fxSoundVar;
        if (collision.gameObject.CompareTag("Coin"))
        {
            audioSource.PlayOneShot(audioClip1);
            scoreValue += UnityEngine.Random.Range(100, 200);
            scoreText.text = Convert.ToString(scoreValue);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Health"))
        {
            health = 6;
            audioSource.PlayOneShot(audioClip2);
            healthValue = 100;
            healthText.text = "100";
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Armor"))
        {
            armor = 6;
            audioSource.PlayOneShot(audioClip2);
            armorValue = 100;
            armorText.text = "100";
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("BulletPowerUp"))
        {
            audioSource.PlayOneShot(audioClip2);
            if (bulletsIndex != 11) { bulletsIndex++; Destroy(collision.gameObject); }
            else { Destroy(collision.gameObject); }
        }
        else if (collision.gameObject.CompareTag("WhichWeapon"))
        {
            audioSource.PlayOneShot(audioClip2);
            bulletsIndex = UnityEngine.Random.Range(0, 12);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("AtomicBomb"))
        {
            audioSource.PlayOneShot(audioClip2);
            var allGameObjects = GameObject.FindGameObjectsWithTag("Enemy");
            for (int numbers = 0; numbers < allGameObjects.Length; numbers++)
            {
                Instantiate(explosionAnim, allGameObjects[numbers].transform.position, Quaternion.identity);
                Destroy(allGameObjects[numbers]);
                LevelManager.enemiesNumberIndex--;
                if (LevelManager.enemiesNumberIndex == 0) { LevelManager.timeIndex++; EnemyGroupAwake.enemyGroupCounter++; LevelManager.lastTime = Time.time; LevelManager.levelManagerInstance.enabled = true; }
            }
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("DeathScore"))
        {
            audioSource.PlayOneShot(audioClip2);
            CharacterReaction();
            LevelManagerOn();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("BigCoin"))
        {
            audioSource.PlayOneShot(audioClip1);
            scoreValue += UnityEngine.Random.Range(1000, 2000);
            scoreText.text = Convert.ToString(scoreValue);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Asteroid"))
        {
            LevelManagerOn();
            CharacterReaction();
        }
        else if (collision.gameObject.CompareTag("enemy_bullet"))
        {
            if (armor > 0)
            {
                armor -= enemyBulletPower;
                armorValue -= armorHolder;
                armorText.text = Convert.ToString(Mathf.Clamp(armorValue, 0, 100));
            }
            else
            {
                armorText.text = "0";
                health -= enemyBulletPower;
                healthValue -= healthHolder;
                healthText.text = Convert.ToString(Mathf.Clamp(healthValue, 0, 100));
                if (health <= 0)
                {
                    Instantiate(explosionAnim, transform.position, Quaternion.identity);
                    LevelManagerOn();
                    Destroy(gameObject);
                }
            }
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            LevelManagerOn();
            CharacterReaction();
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            LevelManagerOn();
            CharacterReaction();
        }
        else if (collision.gameObject.CompareTag("PowerFullBossBullet"))
        {
            LevelManagerOn();
            CharacterReaction();
        }
    }
    private void CharacterReaction()
    {
        healthValue = 0;
        armorValue = 0;
        scoreValue = 0;
        healthText.text = "0";
        armorText.text = "0";
        Instantiate(explosionAnim, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void LevelManagerOn()
    {
        if (WaveTextShowerAndHider.waveTextShowerAndHiderInstance.enabled == true) { WaveTextShowerAndHider.waveTextShowerAndHiderInstance.enabled = false; }
        BackMusicController.backgroundAudioSource.mute = true;
        isDead = true;
        LevelManager.levelManagerInstance.enabled = true;
        LevelManager.lastTime = Time.time;
        LevelManager.timeIndex = 2;
    }
}
