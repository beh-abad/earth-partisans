using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMovement : MonoBehaviour
{
    [SerializeField]
    private const int alternativeRange = 1;
    private int alternativeDirect = 1;
    [SerializeField]
    private float speed;
    public static SinMovement sinMovementInstance;
    private void Awake()
    {
        sinMovementInstance = this;
    }  
    void FixedUpdate()
    {
        if (transform.position.x > 0.25f) { transform.position = new Vector2(0.25f, transform.position.y); alternativeDirect = -alternativeRange; }
        else if (transform.position.x < -0.25f) { transform.position = new Vector2(-0.25f, transform.position.y); alternativeDirect = alternativeRange; }
        transform.position += new Vector3(speed * alternativeDirect * Time.deltaTime, 0, 0);
    }
}