using UnityEngine;
using UnityEngine.UI;

public class TextRenderer : MonoBehaviour
{
    private WaveTextShowerAndHider mediator;
    [SerializeField]
    private GameObject canvas;
    private void Start()
    {
        mediator = GetComponent<WaveTextShowerAndHider>();
    }
    private void Update()
    {
        TextRendererFunction();
    }
    private void TextRendererFunction()
    {
        WaveTextShowerAndHider.timeToHideOrShow = Time.time;
        mediator.enabled = true;
        if (LevelManager.waves != 1)
        {
            mediator.waveText.text = LevelManager.waveCounter + " Z¼¶";
        }
        else
        {
            mediator.waveText.text = "oiA Z¼¶";
        }
        enabled = false;
    }
}
