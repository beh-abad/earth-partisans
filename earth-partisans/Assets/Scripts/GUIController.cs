using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    public static float fxSoundVar, musicVar;
    private readonly string isSecondTime = "SecondState", musicSavePlace = "musicSavePlace", fxSavePlace = "fxSavePlace";   
    [SerializeField]
    private Button button;
    [SerializeField]
    private Sprite sprite1, sprite2;
    [SerializeField]
    private Slider slider1, slider2;
    private void Start()
    {
        if (PlayerPrefs.GetInt(isSecondTime) == 0)
        {
            FirstValues();
            PlayerPrefs.SetInt(isSecondTime, 1);
            SaveChanges(musicVar, fxSoundVar);
        }
        else
        {
            musicVar = PlayerPrefs.GetFloat(musicSavePlace);
            fxSoundVar = PlayerPrefs.GetFloat(fxSavePlace);
            slider1.value = musicVar;
            slider2.value = fxSoundVar;
        }
    }     
    public void OnMusicSliderChanges()
    {
        musicVar = slider1.value;
    }
    public void OnFXSliderChanges()
    {
        fxSoundVar = slider2.value;
    }
    private void SaveChanges(float value1, float value2)
    {
        PlayerPrefs.SetFloat(musicSavePlace, value1);
        PlayerPrefs.SetFloat(fxSavePlace, value2);
    }
    public void ResetFunction()
    {
        FirstValues();
        SaveChanges(musicVar, fxSoundVar);
    }
    private void FirstValues()
    {
        musicVar = .5f;
        fxSoundVar = .5f;
        slider1.value = musicVar;
        slider2.value = fxSoundVar;
    }
}