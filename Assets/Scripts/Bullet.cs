﻿using UnityEngine;

public class Bullet : MonoBehaviour {

    private Transform target;

    public float speed = 30f;

    public int damage = 50;

    public float explosionRadius = 0f;

    public void Seek(Transform _target)
    {
        target = _target;
    }
	// Update is called once per frame
	void Update () {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame) 
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
	}
    void HitTarget()
    {
       // if (explosionRadius > 0f)
        //{
         //   Explode();
        //}
       // else
        //{
            Damage(target);
       // }
        Destroy(gameObject);
    }
    /*void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }*/

    void Damage(Transform target)
    {
        Enemy e = target.GetComponent<Enemy>();
        Turret t = target.GetComponent<Turret>();
        ShootingEnemy eAtt = target.GetComponent<ShootingEnemy>();

        if (e != null)
            e.TakeDamage(damage);
        if (t != null)
            t.TakeDamage(damage);
        if (eAtt != null)
            eAtt.TakeDamage(damage);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
