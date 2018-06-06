using UnityEngine;
using UnityEngine.UI;

public class ShootingEnemy : MonoBehaviour {

    public float range = 2f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    public string turretTag = "Turret";
    public GameObject bulletPrefab;

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
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    /// <summary>
    /// Susiranda target kuris yra range, kad galetu ji pult
    /// </summary>
    void UpdateTarget()
    {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag(turretTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestTurret = null;

        foreach (GameObject turret in turrets)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, turret.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestTurret = turret;
            }
        }

        if (nearestTurret != null && shortestDistance <= range)
        {
            targetToShoot = nearestTurret.transform;
        }
        else
        {
            targetToShoot = null;
        }
    }


    public void TakeDamage(int dmgAmount)
    {
        Debug.Log("gaunu");
        health -= dmgAmount;

        HealthBar.fillAmount = health / maxHealth;

        if (health <= 0)
            Die();
    }

    void Die()
    {
        PlayerStats.Gold += bountyOnEnemy;
        PlayerStats.Score += bountyOnEnemy * 10;

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
        if (targetToShoot != null)
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        //sounds.TowerShot.Play(); //galimi sounds

        if (bullet != null)
            bullet.Seek(targetToShoot);
    }

    /// <summary>
    /// 
    /// </summary>
    /// 
    void RotateHealthBar(int angle, Transform child)
    {
        switch (angle)
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

    void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);

    }
}
