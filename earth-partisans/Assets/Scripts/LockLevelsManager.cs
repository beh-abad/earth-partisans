using UnityEngine.UI;
using UnityEngine;

public class LockLevelsManager : MonoBehaviour
{
    private int var;
    [SerializeField]
    private Button[] buttons;
    [SerializeField]
    private Image[] images;
    [SerializeField]
    private Text[] texts;     
    void Start()
    {      
         var = PlayerPrefs.GetInt("LevelsPassed");  
        if (var == 10) { var = 9; }
        for (int i = 9; i > var; i--)
        {
            buttons[i].interactable = false;
            images[i].gameObject.SetActive(true);
            texts[i].gameObject.SetActive(false);
        }
    }
}