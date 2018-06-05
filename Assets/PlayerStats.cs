using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public static int Gold;
    public int startGold = 400;

    public static int Lives;
    public int startLives = 20;

    public static int Score;

    void Start()
    {
        Gold = startGold;
        Lives = startLives;
        Score = 0;
    }

}
