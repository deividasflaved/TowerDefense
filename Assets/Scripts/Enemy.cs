using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour {


    public float speed = 1f; //Judėjimo greitis
    public Transform child;

    public int health = 100;
    private float maxHealth;
    public int bountyOnEnemy = 10;

    private Transform target; //Sekantis taškas kur judėt
    private Transform targetToShoot;
    private int wavepointIndex = 0; //Sekančio taško index

    public Image HealthBar;

    void Start()
    {
        maxHealth = health;
        target = Waypoints.points[0];//Pradžiai parenkam pirma tašką
    }

    public void TakeDamage(int dmgAmount)
    {
        health -= dmgAmount;//dmg gavimas

        HealthBar.fillAmount = health / maxHealth;//atnaujinamas health bar

        if (health <= 0)//mirti jei nėra gyvybių
            Die();
    }

    void Die()
    {
        PlayerStats.Gold += bountyOnEnemy;
        PlayerStats.Score += bountyOnEnemy * 10;//taškai skaičiuojami "priešo vertė"*10
                                                //skirtingi priešai turi skirtinga vertę tai ir skirtingai skaičiuojama

        WaveSpawner.EnemiesAlive--;

        Destroy(gameObject);
        
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;//Gaunam kryptį        
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);//Judėjimas

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        int angleInt = Mathf.RoundToInt(angle);
        if (angleInt == 1)//laikinas bugfixas kai angleInt būna 1
            angleInt = 0;
        Quaternion q = Quaternion.AngleAxis(angleInt, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 100);
        RotateHealthBar(angleInt, child);

        if (Vector3.Distance(transform.position, target.position) <= 0.02f)//Jei pasiekėm tašką paimtų kitą
             GetNextWaypoint();
    }

    /// <summary>
    /// 
    /// </summary>
    /// 
    void RotateHealthBar(int angle,Transform child)
    {
        switch(angle)
        {
            case 0:
                child.transform.localPosition = new Vector3(0.0f, 0.2f, 0.0f);
                child.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                break;
            case 90:
                child.transform.localPosition = new Vector3(0.2f, 0.0f, 0.0f);
                child.transform.localEulerAngles = new Vector3(0.0f, 0.0f, -90.0f);
                break;
            case -90:
                child.transform.localPosition = new Vector3(-0.2f, 0.0f, 0.0f);
                child.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 90.0f);
                break;
            case 180:
                child.transform.localPosition = new Vector3(0.0f, -0.2f, 0.0f);
                child.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 180.0f);
                break;
            case -180:
                child.transform.localPosition = new Vector3(0.0f, -0.2f, 0.0f);
                child.transform.localEulerAngles = new Vector3(0.0f, 0.0f, -180.0f);
                break;
        }
    }
    /// <summary>
    /// waypoint gavimas
    /// </summary>
    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    /// <summary>
    /// jei kelio pabaiga
    /// </summary>
    void EndPath()
    {
        PlayerStats.Lives--;//praranda gyvybę
        WaveSpawner.EnemiesAlive--;//gyvų priešų skaitiklis sumažėja vienu
        Destroy(gameObject);//sunaikinamas priešas
            
    }
}
