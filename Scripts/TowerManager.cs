using System;
using Unity.Collections.Tests.CoreCLR.TestJobs;
using Unity.VisualScripting;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    // Important Data
    [Header("Attributes")]
    public float baseDamage;
    public float attackRange;
    public float fireRate;
    private float fireCountdown = 0f;

    [Header("Unity Setup Fields")]
    public Transform partToRotate; // Part for the tracking
    public float turnSpeed = 10f;
    public string enemyTag = "Enemy";
    private Transform target; // Notes what the tower will prioritize shooting

    [Header("Bullet Components")]
    public GameObject projectilePrefab;
    public Transform firePoint;
   
    void Start()
    {
        baseDamage = 1f;
        attackRange = 7f;
        fireRate = 1f;
        InvokeRepeating("UpdateTarget", 0f, 0.1f);
    }

    void UpdateTarget()
    {
        // Finds all enemies within turret's range
        GameObject[] closeEnemies = GameObject.FindGameObjectsWithTag(enemyTag);
        // Two Temp variables, one holds the distance to the closest enemy, and the other stores the GameObject of the closest enemy
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in closeEnemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            { 
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= attackRange)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
    
    void Update()
    {
        // If there's no target, nothing will update
        if (target == null)
        {
            return;
        }

        // How we know what enemy to track
        Vector2 dir = target.position - partToRotate.position; // Gets direction turret vector from turret to enemy
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; // Calculates the angle. Goes from radians to degrees
        float offset = -90f; // partToRotate starts -90 degrees off, so this is the offset
        Quaternion lookRotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
        partToRotate.rotation = Quaternion.RotateTowards(partToRotate.rotation, lookRotation, turnSpeed * Time.deltaTime * 50f); // Rotates towards the target on the Z-axis


        // If you can, Shoot() the enemy
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime; // Every second fireCountdown will decrease
    }

    void Shoot()
    {
        // Creates a temp GameObject for fired bullets
        GameObject firedBullet = (GameObject)Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        BulletPhysics bullet = firedBullet.GetComponent<BulletPhysics>();

        // If a bullet actually IS fired, we call Seek() with target as the argument
        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    void OnDrawGizmos() // Visually shows turret range
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
