using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    //Naudojamas užkrauti pagrindiniai scenai
	public void PlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
        WaveSpawner.EnemiesAlive = 0;
    }
    //naudojams sugrįžti į pagrindinį meniu
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    //naudojamas išeiti iš game
    public void QuitGame()
    {
        Application.Quit();
    }

    //Pause ir Unpause
    public void Pause()
    {
        Time.timeScale = 0f;
    }
    public void Unpause()
    {
        Time.timeScale = 1f;
    }
}
