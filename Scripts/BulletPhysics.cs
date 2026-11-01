using UnityEngine;

public class BulletPhysics : MonoBehaviour
{
    public float speed = 70f;
    // Passes on the Target found under TowerManager.cs
    private Transform target;
    public void Seek (Transform currTarget)
    {
        target = currTarget;
    }

    void Start()
    {
        // Destroys bullets after 5 seconds.
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        // if there is no target, return until one is given
        if (target == null)
        {
            return;
        }

        
        Vector2 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // Moves bullet towards target
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    // Upon hitting enemy, Deal Damage and destroy projectilePrefab
    void HitTarget()
    {
        Destroy(gameObject);
    }
}
