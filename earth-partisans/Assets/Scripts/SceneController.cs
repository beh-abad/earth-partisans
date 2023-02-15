using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject waitingPanel,attentionPanel2;
    private void Start()
    {      
        if (PlayerPrefs.GetInt(PauseMenuController.isSecondTime) == 0)
        {
            PauseMenuController.musicVar = .5f;
            PauseMenuController.fxSoundVar = .5f;
            BackMusicController.backgroundAudioSource.volume = PauseMenuController.musicVar;
            PlayerPrefs.SetInt(PauseMenuController.isSecondTime, 1);
            PauseMenuController.SaveChanges(PauseMenuController.musicVar, PauseMenuController.fxSoundVar);
        }
        else
        {
            PauseMenuController.musicVar = PlayerPrefs.GetFloat(PauseMenuController.musicSavePlace);
            PauseMenuController.fxSoundVar = PlayerPrefs.GetFloat(PauseMenuController.fxSavePlace);
            BackMusicController.backgroundAudioSource.volume = PauseMenuController.musicVar;
        }
    }
    public void LoadScene(string sceneName)
    {
        waitingPanel.SetActive(true);
        SceneManager.LoadSceneAsync(sceneName);
    }
    public void OffAttentionPanel()
    {
        attentionPanel2.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
