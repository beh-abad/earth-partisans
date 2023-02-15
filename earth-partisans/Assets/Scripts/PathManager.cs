using System.Collections;
using UnityEngine;

public class PathManager : MonoBehaviour
{

    [SerializeField]
    private bool canStayThere, hasLastPathIndex, hasRepeat, isBoss;//Path states
    private bool coroutineAllowed;
    [SerializeField]
    private uint lastPathIndex;//Specify last path index for shifting enemies
    private uint pathIndex;//Enemies pathes index
    public Transform[] enemyPathes;//Enemies pathes  
    private Vector3 enemyPosition;
    [SerializeField]
    private float enemySpeed;
    private float alternativeTime;
    public static PathManager instance2;
    void Start()
    {
        pathIndex = 0;
        alternativeTime = 0;
        coroutineAllowed = true;
        instance2 = this;
    }
    void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(EnemyPathFunction());
        }
    }
    private IEnumerator EnemyPathFunction()
    {
        coroutineAllowed = false;
        Vector2 point1 = enemyPathes[pathIndex].GetChild(0).transform.position;//CS 1
        Vector2 point2 = enemyPathes[pathIndex].GetChild(1).transform.position;//CS 2
        Vector2 point3 = enemyPathes[pathIndex].GetChild(2).transform.position;//CS 3
        Vector2 point4 = enemyPathes[pathIndex].GetChild(3).transform.position;//CS 4
        while (alternativeTime < 1)
        {
            yield return new WaitForEndOfFrame();
            alternativeTime += Time.deltaTime * enemySpeed * Time.timeScale;
            enemyPosition = Mathf.Pow(1 - alternativeTime, 3) * point1 + 3 * Mathf.Pow(1 - alternativeTime, 2) * alternativeTime * point2 +
                3 * (1 - alternativeTime) * Mathf.Pow(alternativeTime, 2) * point3 + Mathf.Pow(alternativeTime, 3) * point4;
            transform.position = enemyPosition;
        }
        alternativeTime = 0;
        pathIndex += 1;
        if (pathIndex == lastPathIndex && hasLastPathIndex == true)//If pathIndex = lastPathIndex we can shift that point so enemies can shift also we have more than one pathes 
        {
            EnemyGroupAwake.instance.ShifterFunction();
        }
        if (pathIndex > enemyPathes.Length - 1)//If pathes have done
        {
            if (isBoss)
            {
                RandomPath.randomPathInstance.enabled = true;
            }
            else if (canStayThere == true)//Enemies can stay there
            {               
                Destroy(this);
            }
            else if (hasRepeat == true)//Or repeat that path
            {
                pathIndex = 0;
            }
        }
        coroutineAllowed = true;
    }
}