using UnityEngine;
using System.Collections;
using TMPro;

public class WaveSpawner : MonoBehaviour {

    public static int EnemiesAlive = 0;

    public Wave[] waves;
    public Wave boss;

    public Transform spawnPoint;

    public AudioSource waveSpawnSound;

    public float timeBetweenWaves = 5f;
    public float countdown = 0f;
    private float timeBetweenEnemies = 0.5f;

    private bool isSpeeded = false;
    private bool isFirstWave = true;
    private bool isCoroutineStarted = false;

    private int waveNumber = 0;
    private int diff = 1;

    public TextMeshProUGUI timerText;

    void Update()
    {
        if (EnemiesAlive > 0)//jei gyvų priešų yra nieko nedaryti
            return;

        if (countdown <= 0f && !isFirstWave)// jei pirmas wave ir countdown 0 nieko nedaryti
        {                                   // kitu atveju spawn wave pagal time po pirmo wave(yra galimybė ir pačiam paleist wave nepasibaigus laikui)
            StartCoroutine(SpawnWave()); //pradeda spawn coroutine
            countdown = timeBetweenWaves; //timeris
            return;
        }
        countdown -= Time.deltaTime; //timeris mažėjai pagal mūsų Time, o ne frmaes

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        timerText.text = string.Format("{0:00.00}", countdown);
    }
    public void StartCoroutineViaButton() //Galimybė pradėti coroutine mygtuko paspaudimu(pirmam wave būtina paspaust)
    {
        if (isCoroutineStarted)
            return;
        StartCoroutine(SpawnWave());
        isFirstWave = false;
        countdown = timeBetweenWaves;
        timerText.text = string.Format("{0:00.00}", 0f);
    }
    public void SpeedUp()//Galėjimas pagreitinti eiga
    {
        if (!isSpeeded)
        {
            Time.timeScale = 2f;
            isSpeeded = true;
        }
        else
        {
            Time.timeScale = 1f;
            isSpeeded = false;
        }
    }
    IEnumerator SpawnWave()//spawn coroutine
    {
        isCoroutineStarted = true;
        waveSpawnSound.Play();//garsas paspawninus wave
        Wave wave = waves[waveNumber];//iš visų wave array paimamas atitinkamas wave kurį ir spawninam

        for (int i = 0; i < wave.amountOfEnemiesToSpawn*diff; i++)//ciklas spawnint visus wave enemy
        {
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        if ((waveNumber + 1) % 5 == 0)//kas penkta wave boss tipas
            SpawnEnemy(boss.enemyPrefab);
        waveNumber++;//padidinam wave numerį

        if (waveNumber == waves.Length)
        {
            Debug.Log("Level won!");//TODO galima būtų padaryt ekrana kad laimėjai, bet dabar sąlyga tik kad pralaimėsim
            //this.enabled = false;
            diff++;
            waveNumber = 0;
        }
        isCoroutineStarted = false;
    }
    void SpawnEnemy(GameObject enemyPrefab)//spawn enemy metodas
    {
        Instantiate(enemyPrefab,spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;
    }
}
