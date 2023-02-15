using UnityEngine;
using UnityEngine.UI;
public class BossHealthController : MonoBehaviour
{
    public Slider slider;
    public static BossHealthController bossHealthControllerInstance;
    private void Awake()
    {
        bossHealthControllerInstance = this;
        gameObject.SetActive(false);
    }
    public void SetValues(int maxValue)
    {
        slider.maxValue = maxValue;
        slider.value = maxValue;
    }
    public void UseValue(int value)
    {
        slider.value = value;
    }
}
