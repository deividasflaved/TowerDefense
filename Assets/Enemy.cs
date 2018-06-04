using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed = 1f; //Judėjimo greitis

    private Transform target; //Sekantis taškas kur judėt
    private int wavepointIndex = 0; //Sekančio taško index

    void Start()
    {
        target = Waypoints.points[0];//Pradžiai parenkam pirma tašką
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;//Gaunam kryptį
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);//Judėjimas

        if (Vector3.Distance(transform.position, target.position) <= 0.02f)//Jei pasiekėm tašką paimtų kitą
            GetNextWaypoint();
    }
    /// <summary>
    /// kito taško paėmimas
    /// </summary>
    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }
}
