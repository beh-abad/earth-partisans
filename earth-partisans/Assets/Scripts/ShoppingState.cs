using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShoppingState : MonoBehaviour
{
    [SerializeField]
    private GameObject[] notBoughtButton, boughtButton, chooseImage;
    [SerializeField]
    private Button[] buttons;
    [SerializeField]
    private Text[] texts;
    void Start()
    {
        Application.targetFrameRate = 100;
        for (int i = 0; i < 12; i++)
        {
            if (PlayerPrefs.GetInt(buttons[i].name) == 1)
            {
                texts[i].text = "0";
                boughtButton[i].SetActive(true);
                notBoughtButton[i].SetActive(false);
            }
            if (PlayerPrefs.GetInt("lastChoose") == i)
            {
                texts[i].text = "0";
                chooseImage[i].SetActive(true);
                boughtButton[i].SetActive(false);
                notBoughtButton[i].SetActive(false);
            }
        }
    }   
}
