using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingController : MonoBehaviour
{
    public static float fxSoundVarSetting, musicVarSetting;
    public static readonly string isSecondTime = "SecondState", musicSavePlace = "musicSavePlace", fxSavePlace = "fxSavePlace";
    [SerializeField]
    private GameObject waitingPanel;
    [SerializeField]
    private Slider slider1, slider2;
    private void Start()
    {
        musicVarSetting = PlayerPrefs.GetFloat(PauseMenuController.musicSavePlace);
        fxSoundVarSetting = PlayerPrefs.GetFloat(PauseMenuController.fxSavePlace);
        slider1.value = musicVarSetting;
        slider2.value = fxSoundVarSetting;
    }
    public void OnMusicSliderChanges()
    {
        musicVarSetting = slider1.value;
    }
    public void OnFXSliderChanges()
    {
        fxSoundVarSetting = slider2.value;
    }
    public static void SaveChanges(float value1, float value2)
    {
        PlayerPrefs.SetFloat(musicSavePlace, value1);
        PlayerPrefs.SetFloat(fxSavePlace, value2);
    }
    public void ResetFunction()
    {
        FirstValues();
        SaveChanges(musicVarSetting, fxSoundVarSetting);
    }
    public void ActionsAfterClicked()
    {
        BackMusicController.backgroundAudioSource.volume = musicVarSetting;
        SaveChanges(musicVarSetting, fxSoundVarSetting);       
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadSceneAsync("(A)GUI");             
        waitingPanel.SetActive(true);
    }
    private void FirstValues()
    {
        musicVarSetting = .5f;
        fxSoundVarSetting = .5f;
        slider1.value = musicVarSetting;
        slider2.value = fxSoundVarSetting;
    }

}