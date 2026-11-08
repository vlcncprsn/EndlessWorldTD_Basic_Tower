/***************************************************************
*file: BulletPhysics.cs
*author: G. Valdiviez
*class: CS 4700 – Game Development
*assignment: Final Program
*date last modified: 11/8/2026
*
*purpose: Contains the physics behind the 'Bullet' Prefab.
*         Gets the bullet to interact with enemies if they collide.
*         Causes the bullet to despawn if the bullet hits an enemy or it flies for 5 seconds.
*
****************************************************************/

using UnityEngine;

public class BulletPhysics : MonoBehaviour
{
    public float speed = 7f;
    // Passes on the Target found under TowerManager.cs
    private Vector2 direction;
    private float damage;

    // function: Seek
    // Purpose: When called from TowerManager.cs, gives the projectile two pieces of information:
    //          The direction the bullet is being fired.
    //          The data of the firing tower, primarily for baseDamage.
    public void Seek (Transform currTarget, float dmg)
    {
        direction = ((Vector2)currTarget.position - (Vector2)transform.position).normalized;
        damage = dmg;
    }

    // function: Start
    // Purpose: Called on the frame when this script is enabled.
    //          Destroy any bullets that last at least 5 seconds flying.
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    // function: Update
    // Purpose: Called every frame while the script is active
    //          Checks to see if there is a target, if not the return
    //          If there is then we see how close the projectile is to the enemy.
    //          If the magnitude of the target's position is less than the amount of distance traveled in a frame, call HitTarget and return.
    //          Else translate the bullet in the direction of the enemy by distanceThisFrame
    void Update()
    {
        // Moves bullet towards target
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    // function: OnTriggerEnter2D
    // Purpose: Detects if the projectile actually collides with an enemy.
    //          If it does then the enemy loses health and the projectile is destroyed.
    //          If not it'll fly until Start() gets to it.
    void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.GetComponentInParent<EnemyHealth>();

        if (enemyHealth == null)
        {
            return;
        }

        enemyHealth.TakeDamage(damage);
        Destroy(gameObject);
    }
}
