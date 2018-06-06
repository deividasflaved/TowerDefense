using TMPro;
using UnityEngine;

public class HighScoreUI : MonoBehaviour {

    public TextMeshProUGUI highScoreText;
    public string highScoreKey = "HighScore";

    void OnEnable()
    {
        highScoreText.text = "";
        for (int i = 1; i <= 5; i++)
        {
            highScoreText.text += i + "  -  " + PlayerPrefs.GetInt(highScoreKey + i.ToString())+"\n";
        }
    }
}
