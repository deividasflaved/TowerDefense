using UnityEngine;

public class GameManager : MonoBehaviour {

    private bool gameOver = false;
    public string highScoreKey = "HighScore";
    public GameObject gameOverUI;
    void Update()
    {
        if (gameOver)
            return;

        if (PlayerStats.Lives <= 0)
            EndGame();
    }
    void EndGame()
    {
        gameOver = true;
        gameOverUI.SetActive(true);
        SetHighScores();
    }
    void SetHighScores()
    {
        int replaceIndex = 0;
        for (int i = 1; i <= 5; i++)
        {
            if (PlayerStats.Score > PlayerPrefs.GetInt(highScoreKey + i.ToString()) && replaceIndex == 0) 
                replaceIndex = i;    
        }
        if (replaceIndex == 0)
            return;
        else
            for (int i = 5; i > replaceIndex; i--)
            {
                PlayerPrefs.SetInt(highScoreKey + i.ToString(), PlayerPrefs.GetInt(highScoreKey + (i - 1).ToString()));
            }
        PlayerPrefs.SetInt(highScoreKey + replaceIndex.ToString(), PlayerStats.Score);
    }
}
