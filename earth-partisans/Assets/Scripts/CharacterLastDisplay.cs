using UnityEngine;

public class CharacterLastDisplay : MonoBehaviour
{
    [SerializeField]
    private int speed;
    public static CharacterLastDisplay characterLastDisplayInstance;
    void Start()
    {
        characterLastDisplayInstance = this;
        enabled = false;
    }
    void Update()
    {      
        transform.position += new Vector3(0, speed * Time.deltaTime);
    }
}
