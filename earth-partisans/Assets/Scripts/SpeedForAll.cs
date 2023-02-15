using UnityEngine;

public class SpeedForAll : MonoBehaviour
{
    [SerializeField]
    private float speed;
    void Update()
    {
        transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
    }
}
