using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Turret : MonoBehaviour {

    private Transform target;

    [Header("Attributes")]

    public float range = 2f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public int lvl = 1;
    public int health = 1000;
    private float maxHealth;

    [Header("Unity Setup Fields")]

    public float turnSpeed = 25f;
    public string enemyTag = "Enemy";

    public GameObject bulletPrefab;
    public Transform firePoint;
    public GameObject lvlUi;
    public TextMeshProUGUI lvlText;

    public Image HealthBar;

    SoundsControler sounds;


	void Start ()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        sounds = SoundsControler.instance;
        maxHealth = health;
	}
    /// <summary>
    /// suka tower pagal target
    /// skaičiuoja cooldown tarp šūvių
    /// </summary>
    void Update()
    {
        if (target == null)
            return;

        Vector3 vectorToTarget = target.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * turnSpeed);

        lvlUi.transform.rotation = Quaternion.Euler(0.0f, 0.0f, transform.rotation.z * -1.0f);


        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }
    /// <summary>
    /// funkcija skirta ieškoti target kas tam tikra intervala(0.5sec)
    /// </summary>
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);//susiranda visus enemy
        float shortesDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)//ieško trumpiausio atstumo iki enemy
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortesDistance)
            {
                shortesDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy!= null && shortesDistance <= range)//pasirenka artimiausia enemy kaip target
        {
            target = nearestEnemy.transform;
        }
        else//jei nieko neranda target yra null
        {
            target = null;
        }
    }
    /// <summary>
    /// bokštelis šauna į priešą
    /// </summary>
    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        sounds.TowerShot.Play();

        if (bullet != null)
            bullet.Seek(target);
    }
    /// <summary>
    /// bokštelis gauna dmg iš priešo
    /// </summary>
    /// <param name="dmgAmount"></param>
    public void TakeDamage(int dmgAmount)
    {
        health -= dmgAmount;

        HealthBar.fillAmount = health / maxHealth;

        if (health <= 0)
            Die();
    }
    /// <summary>
    /// pasibaigus gyvybėms miršta
    /// </summary>
    void Die()
    {

        Destroy(gameObject);

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,range);
    }
}
