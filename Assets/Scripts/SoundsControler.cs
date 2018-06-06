using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsControler : MonoBehaviour {

    public static SoundsControler instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
        }
        instance = this;
    }

    public AudioSource GameOver;
    public AudioSource SellTower;
    public AudioSource NotEnoughResource;
    public AudioSource TowerUpgrade;
    public AudioSource TowerPlaced;
    public AudioSource TowerShot;
    public AudioSource WaveStart;
}
