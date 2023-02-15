using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private bool isWinner;
    public static bool on, asteroidsTurn;
    [HideInInspector]
    public static uint timeIndex;
    public static int waves, waveCounter, enemiesNumberIndex, asteroidsNumberIndex;//Waves of enemies,invert of waves,Numbers of enemies   
    [SerializeField]
    private int waves2, interrupt;
    [HideInInspector]
    public static float lastTime;
    [SerializeField]
    private GameObject tPrefab, losingPanel, winPanel, player;
    private Rigidbody2D spaceshipRb;
    private EnemyGroupAwake enemyGroupAwake2;
    private TextRenderer textRenderer2;
    public static LevelManager levelManagerInstance;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip fx;
    // Start is called before the first frame update 
    void Start()
    {
        Instantiate(player, new Vector2(0, -7), new Quaternion(0, 0, -90, 90));
        spaceshipRb = FindObjectOfType<CharacterController1>().gameObject.GetComponent<Rigidbody2D>();
        isWinner = false;
        on = true;
        asteroidsTurn = false;
        waveCounter = 1;
        enemiesNumberIndex = 24;
        asteroidsNumberIndex = 32;
        waves = waves2;
        timeIndex = 0;
        lastTime = Time.time;
        enemyGroupAwake2 = GetComponent<EnemyGroupAwake>();
        textRenderer2 = tPrefab.GetComponent<TextRenderer>();
        levelManagerInstance = this;
    }
    // Update is called once per frame
    void Update()
    {
        LevelManagerFunction();
    }
    public void LevelManagerFunction()
    {
        if (waves == 1 && enemiesNumberIndex == 0 && !isWinner || waves == 1 && asteroidsTurn == true && !isWinner)
        {
            isWinner = true;
            timeIndex = 3;
        }
        else if (enemiesNumberIndex == 0 && !isWinner)
        {
            on = true;
            waves--;
            waveCounter++;
            EnemyGroupAwake.shiftIndex++;
            enemiesNumberIndex = 24;
            timeIndex = 0;
            lastTime = Time.time;
        }
        else if (asteroidsTurn == true && !isWinner)
        {
            on = true;
            asteroidsTurn = false;
            waves--;
            waveCounter++;
            timeIndex = 0;
            lastTime = Time.time;
        }
        else
        {
            if (Time.time > lastTime + 2 + interrupt && timeIndex == 0 && on == true)
            {
                textRenderer2.enabled = true;
                on = false;
                enabled = false;
            }
            else if (Time.time > lastTime + 2 + interrupt && timeIndex == 1)
            {
                SinMovement.sinMovementInstance.enabled = true;
                enemyGroupAwake2.enabled = true;
                enabled = false;
            }
            else if (Time.time > lastTime + 2 + interrupt && timeIndex == 2 && CharacterController1.isDead)
            {
                CharacterController1.isDead = false;
                losingPanel.SetActive(true);
                audioSource.volume = PauseMenuController.fxSoundVar;
                audioSource.PlayOneShot(fx);
                Time.timeScale = 0;
                enabled = false;
            }
            else if (Time.time > lastTime + 2 + interrupt && timeIndex == 3 && isWinner)
            {
                CharacterController1.CharacterControllerInstance.enabled = false;
                CharacterController1.CharacterControllerInstance.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                CharacterLastDisplay.characterLastDisplayInstance.enabled = true;
                lastTime = Time.time;
                timeIndex++;
            }
            else if (Time.time > lastTime + 3 + interrupt && timeIndex == 4 && isWinner)
            {
                BackgroundController.backgroundControllerInstance.enabled = false;
                SinMovement.sinMovementInstance.enabled = false;
                Destroy(GameObject.Find("Spaceship"));
                winPanel.SetActive(true);
                enabled = false;
            }
        }
    }
}