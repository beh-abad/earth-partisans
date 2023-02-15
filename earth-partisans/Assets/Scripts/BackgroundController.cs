using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private Renderer rb;
    public  Vector2 backSpeed;
    public static BackgroundController backgroundControllerInstance;
    void Awake()
    {
        rb = GetComponent<Renderer>();
        backgroundControllerInstance = this;      
    }
    void FixedUpdate()
    {      
       rb.material.mainTextureOffset = backSpeed * Time.time;     
    }    
}
