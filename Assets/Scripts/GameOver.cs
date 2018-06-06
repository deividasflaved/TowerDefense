using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOver : MonoBehaviour {

    public TextMeshProUGUI scoreText;
    public string highScoreKey = "HighScore";

    void OnEnable()
    {

        scoreText.text = "GAME OVER\n" + "Score: " + PlayerStats.Score.ToString();
        Time.timeScale = 0f;
        int place = 0;
        for (int i = 1; i <= 5; i++)
        {
            if (PlayerStats.Score > PlayerPrefs.GetInt(highScoreKey + i.ToString()) && place == 0)
                place = i;
        }
        if (place != 0) 
            scoreText.text += "\n PLACED - " + place;
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        WaveSpawner.EnemiesAlive = 0;      
    }
}
