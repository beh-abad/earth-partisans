using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MustBeCleaned : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Destroyer"))
        {
            Destroy(gameObject);
        }      
    }
}

