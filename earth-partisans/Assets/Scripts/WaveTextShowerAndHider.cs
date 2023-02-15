using UnityEngine;
using UnityEngine.UI;

public class WaveTextShowerAndHider : MonoBehaviour
{  
    public Text waveText;
    public static float timeToHideOrShow;
    [SerializeField]
    private float speedOfHideOrShow;
    public static WaveTextShowerAndHider waveTextShowerAndHiderInstance;
    private void Start()
    {       
        timeToHideOrShow = Time.time;
        waveTextShowerAndHiderInstance = this;
    }

    private void FixedUpdate()
    {
        WaveTextShowerAndHiderFunction();    
    }
    private void WaveTextShowerAndHiderFunction()
    {
        if (Time.time < timeToHideOrShow + 2)
        {
            waveText.color = Color.Lerp(new Color(waveText.color.r, waveText.color.g, waveText.color.b, waveText.color.a), new Color(waveText.color.r, waveText.color.g, waveText.color.b, 1), Time.deltaTime * speedOfHideOrShow);
        }
        else if (Time.time > timeToHideOrShow + 2)
        {
            waveText.color = Color.Lerp(new Color(waveText.color.r, waveText.color.g, waveText.color.b, waveText.color.a), new Color(waveText.color.r, waveText.color.g, waveText.color.b, 0), Time.deltaTime * (speedOfHideOrShow + 3));
        }
        if (waveText.color.a < 0.01f)
        {
            LevelManager.timeIndex++;
            LevelManager.lastTime = Time.time;
            LevelManager.levelManagerInstance.enabled = true;
            enabled = false;
        }
    }
}