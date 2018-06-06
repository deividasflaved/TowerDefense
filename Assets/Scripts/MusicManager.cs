using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public AudioSource BelowHalf;
    public AudioSource CriticalHealth;
    public AudioSource MainMusic;

    private bool isStartedBelowHalf = false;
    private bool isStartedCritical = false;
    private bool isGameOver = false;

    public int BelowHalfHp;
    public int CriticalHp;

    void Start()
    {
        InvokeRepeating("UpdateMusic", 0f, 1f);
    }
    

    void UpdateMusic()
    {
        if (PlayerStats.Lives <= BelowHalfHp && PlayerStats.Lives >= CriticalHp && !isStartedBelowHalf)
        {
            MainMusic.Stop();
            BelowHalf.Play();
            isStartedBelowHalf = true;
        }
        else if (PlayerStats.Lives < CriticalHp && !isStartedCritical)
        {
            BelowHalf.Stop();
            CriticalHealth.Play();
            isStartedCritical = true;
        }
        else if (PlayerStats.Lives <= 0 && !isGameOver) 
        {
            CriticalHealth.Stop();
            MainMusic.Stop();
            BelowHalf.Stop();
        }
    }
}
