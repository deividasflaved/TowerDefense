using UnityEngine;

public class GameManager : MonoBehaviour {

    private bool gameOver = false;

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
    }
}
