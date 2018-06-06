using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour {

    public TextMeshProUGUI scoreText;

    void Update()
    {
        scoreText.text = "SCORE\n"+PlayerStats.Score.ToString();
    }
}
