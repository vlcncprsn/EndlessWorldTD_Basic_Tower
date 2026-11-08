/***************************************************************
*file: EnemyHealth.cs
*author: G. Valdiviez
*class: CS 4700 – Game Development
*assignment: Final Program
*date last modified: 11/7/2026
*
*purpose: How the enemy health system works (most likely not gonna be in final product but who knows).
*
****************************************************************/
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [Header("Attributes")]
    public float health = 15;

    // function: TakeDamage()
    // Purpose: Called when a projectile collides with an enemy.
    //          Takes an integer from BulletPhysics.cs and subtracts it from the enemy's health pool.
    //          Once it hits 0 or less, the enemy is destroyed.
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
