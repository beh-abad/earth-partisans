using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    private int money;
    private int gunMoney;
    [SerializeField]
    private GameObject attentionPanel, notBoughtButton, boughtButton, chooseImage;
    [SerializeField]
    private Button button;
    public void Shopping()
    {
        money = FirstScoreManager.finalScore;
        gunMoney = Convert.ToInt32(gameObject.GetComponent<Text>().text);
        if (gunMoney <= money)
        {
            money -= gunMoney;
            GetComponent<Text>().text = "0";
            PlayerPrefs.SetInt(FirstScoreManager.scorePlace, money);
            PlayerPrefs.SetInt(button.name, 1);
            boughtButton.SetActive(true);
            notBoughtButton.SetActive(false);
        }
        else
        {
            attentionPanel.SetActive(true);
        }
    }
    public void Selection(int buttonIndex)
    {
        chooseImage.SetActive(true);
        PlayerPrefs.SetInt("lastChoose", buttonIndex);
        boughtButton.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }   
}
