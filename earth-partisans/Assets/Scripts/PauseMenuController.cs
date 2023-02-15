using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public static float fxSoundVar, musicVar;
    public static readonly string isSecondTime = "SecondState", musicSavePlace = "musicSavePlace", fxSavePlace = "fxSavePlace";
    [SerializeField]
    private GameObject pauseMenu, waitingPanel;
    [SerializeField]
    private Button button;
    [SerializeField]
    private Sprite sprite1, sprite2;
    [SerializeField]
    private Slider slider1, slider2;
    private void Start()
    {
        musicVar = PlayerPrefs.GetFloat(musicSavePlace);
        fxSoundVar = PlayerPrefs.GetFloat(fxSavePlace);
    }
    public void OnPauseClicked()
    {
        pauseMenu.SetActive(true);
        button.image.sprite = sprite1;
        Time.timeScale = 0;
        slider1.value = musicVar;
        slider2.value = fxSoundVar;
        BackMusicController.backgroundAudioSource.mute = true;
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
        button.image.sprite = sprite2;
        ActionsAfterClicked();
    }
    public void AgainGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        CharacterController1.scoreValue = 0;
        button.image.sprite = sprite2;
        ActionsAfterClicked();
        waitingPanel.SetActive(true);
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadSceneAsync("(A)GUI");
        Time.timeScale = 1;
        button.image.sprite = sprite2;
        ActionsAfterClicked();
        waitingPanel.SetActive(true);
    }
    public void OnMusicSliderChanges()
    {
        musicVar = slider1.value;
    }
    public void OnFXSliderChanges()
    {
        fxSoundVar = slider2.value;
    }
    public void NextLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
        CharacterController1.scoreValue = 0;
        button.image.sprite = sprite2;
        ActionsAfterClicked();
        waitingPanel.SetActive(true);
    }
    public static void SaveChanges(float value1, float value2)
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
        CharacterController1.scoreValue = 0;
        slider1.value = musicVar;
        slider2.value = fxSoundVar;
    }
    private void ActionsAfterClicked()
    {
        BackMusicController.backgroundAudioSource.volume = musicVar;
        SaveChanges(musicVar, fxSoundVar);
        pauseMenu.SetActive(false);
        BackMusicController.backgroundAudioSource.mute = false;
    }
}