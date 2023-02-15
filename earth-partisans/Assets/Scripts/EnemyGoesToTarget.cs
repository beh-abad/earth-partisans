using System;
using UnityEngine;

public class EnemyGoesToTarget : MonoBehaviour
{
    [SerializeField]
    private float enemySpeed;
    private GameObject target;
    void Start()
    {
        try
        {
            target = FindObjectOfType<CharacterController>().gameObject;
        }
        catch (NullReferenceException)
        {
            enabled = false;
        }
    }
    void FixedUpdate()
    {
        try
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            transform.position += new Vector3(direction.x * Time.deltaTime * enemySpeed, direction.y * Time.deltaTime * enemySpeed, 0);
        }
        catch (MissingReferenceException)
        {
            Destroy(this);
        }
    }
}
