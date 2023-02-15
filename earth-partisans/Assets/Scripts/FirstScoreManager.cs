using UnityEngine;
using UnityEngine.UI;

public class FirstScoreManager : MonoBehaviour
{
    public static int finalScore;
    public static readonly string scorePlace = "scorePlace";
    [SerializeField]
    private Text finalScoreText;
    void Start()
    {
        finalScore = PlayerPrefs.GetInt(scorePlace);
        finalScoreText.text = finalScore.ToString();
    }   
}
