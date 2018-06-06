using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public static int Gold;//auksas žaidimo metu
    public int startGold = 400;//auksas pradžioj

    public static int Lives;//gyvybės žaidimo metu
    public int startLives = 20;//gyvybės pradžioj

    public static int Score;//score

    void Start()
    {
        Gold = startGold;
        Lives = startLives;
        Score = 0;
    }

}
