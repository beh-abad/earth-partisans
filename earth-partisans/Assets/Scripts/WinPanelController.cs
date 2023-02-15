using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Android;

public class WinPanelController : MonoBehaviour
{
    private float timeHolder;
    private int score, score2;
    [SerializeField]
    private GameObject winPanel, player;
    [SerializeField]
    private Text scoreCounter, collectedScores;
    [SerializeField]
    private GameObject winPanelTransmit;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip fx;
    public static WinPanelController winPanelControllerInstance;

    void Start()
    {
        int currentScene = (SceneManager.GetActiveScene().buildIndex - 4);
        if (PlayerPrefs.GetInt("LevelsPassed") == currentScene)
        {
            PlayerPrefs.SetInt("LevelsPassed", currentScene + 1);
        }
        timeHolder = Time.time;
        score = CharacterController1.scoreValue;
        collectedScores.text = score.ToString();
        FirstScoreManager.finalScore += score;
        PlayerPrefs.SetInt("scorePlace", FirstScoreManager.finalScore);
        score2 = 0;
        winPanelTransmit.SetActive(true);
        winPanelControllerInstance = this;
        Destroy(SinMovement.sinMovementInstance.gameObject);
    }
    void Update()
    {
        if (Time.time > timeHolder + 0.02f)
        {
            audioSource.volume = PauseMenuController.fxSoundVar;
            audioSource.PlayOneShot(fx);
            score -= 52;
            score2 += 53;
            if (score <= 0) { scoreCounter.text = "0"; enabled = false; }
            else { scoreCounter.text = score.ToString(); collectedScores.text = score2.ToString(); }
            timeHolder = Time.time;
        }
    }
}
