using UnityEngine;
using System;
public class EnemyGroupAwake : MonoBehaviour
{
    [SerializeField]
    private bool hasShifter, hasAstroid, hasBoss;
    [SerializeField]
    private byte bossNumbers;
    public static byte bossNumbersHolder;  
    [SerializeField]
    private int[] wavesOfAsteroids;
    public int pathNumbers;//Which enemies can be born in which wave,enemy types in waves,enemy path numbers
    public static int enemyGroupCounter, shiftIndex;
    private int enemyTypeIndex, numbers;
    private float timeHolder, offset;//Holds last time
    [SerializeField]
    private float enemyBornBeat, asteroidBornBeat;//Enemies per time, asteroids per time   
    [SerializeField]
    private Transform enemyGroupBornPosition, asteroidBornPosition, sinParentPoint;//, , ,
    [SerializeField]
    private Transform[] shiftPoint;
    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private GameObject[] enemy, asteroids;
    public static EnemyGroupAwake instance;
    private void Start()
    {
        enemyTypeIndex = 0;
        numbers = 24;
        enemyGroupCounter = 0;
        shiftIndex = 0;
        timeHolder = Time.time;
        instance = this;
        bossNumbersHolder = bossNumbers;
        if (Screen.width == 1440 && Screen.height == 2960) { offset = 0.6f; }
        else { offset = 0; }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (hasBoss && LevelManager.waves == 1)
        {
            Instantiate(boss, enemyGroupBornPosition.position, Quaternion.identity);
            EnemyGroupState();
        }
        else if (hasAstroid && wavesOfAsteroids[LevelManager.waveCounter - 1] - 1 == LevelManager.waveCounter - 1)
        {
            AsteroidBornController();
        }
        else
        {
            EnemyBornController();
        }
    }
    private void EnemyBornController()
    {
        if (numbers == 0) { EnemyGroupState(); }
        else if (Time.time * Time.deltaTime > (timeHolder + enemyBornBeat) * Time.deltaTime && Time.timeScale != 0)
        {
            EnemyPathSelector();
            if (hasShifter == true)
            {
                ShifterFunction();
            }
            numbers--;
            timeHolder = Time.time;
        }
    }
    private void EnemyPathSelector()
    {
        for (int i = 0; i < pathNumbers; i++)
        {
            enemy[enemyTypeIndex].GetComponent<PathManager>().enemyPathes[i] = enemyGroupBornPosition.transform.GetChild(enemyGroupCounter).transform.GetChild(i);
        }
        Instantiate(enemy[enemyTypeIndex], enemyGroupBornPosition.position, Quaternion.identity, sinParentPoint);
    }
    public void ShifterFunction()
    {
        if (shiftPoint[shiftIndex].transform.position.x >= -1.7 + offset)
        {
            shiftPoint[shiftIndex].transform.position = new Vector2(shiftPoint[shiftIndex].transform.position.x - 0.6f, shiftPoint[shiftIndex].transform.position.y);//Shifts enemies to left       
        }
        else
        {
            shiftPoint[shiftIndex].transform.position = new Vector2(1.93f, shiftPoint[shiftIndex].transform.position.y - 0.6f);//Shifts enemies to down
        }
    }
    private void AsteroidBornController()
    {
        if (numbers == 0 && LevelManager.asteroidsTurn == false) { numbers = 24; LevelManager.asteroidsTurn = true; LevelManager.timeIndex++; LevelManager.lastTime = Time.time; LevelManager.levelManagerInstance.enabled = true; enabled = false; }
        else if (Time.time * Time.deltaTime > (timeHolder + asteroidBornBeat) * Time.deltaTime && Time.timeScale != 0)
        {
            Instantiate(asteroids[UnityEngine.Random.Range(0, 6)], new Vector2(asteroidBornPosition.position.x + UnityEngine.Random.Range(-2.2f, 2.2f), asteroidBornPosition.position.y), Quaternion.identity);
            numbers--;
            timeHolder = Time.time;
        }
    }
    private void EnemyGroupState()
    {
        numbers = 24;
        enemyTypeIndex++;
        LevelManager.timeIndex++;
        enabled = false;
    }
}
