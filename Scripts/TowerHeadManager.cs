/***************************************************************
*file: TowerHeadManager.cs
*author: G. Valdiviez
*class: CS 4700 – Game Development
*assignment: Final Program
*date last modified: 11/6/2026
*
*purpose: Allows the Tower Heads to function as intended by the development team
*         Determines the projectile that a full tower fires, alongside special characteristics depending on the head type.
*         Can be attached to a TowerBase.
*
****************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Collections.Tests.CoreCLR.TestJobs;
using Unity.VisualScripting;
using UnityEngine;

public class TowerHeadManager : MonoBehaviour
{
    [Header("Tower Head Data")]
    public TowerHeadData headData;
    protected TowerBaseManager parentBase;

    [Header("Attack Components")]
    public Transform firePoint;
    private Quaternion defaultRotation;

    [Header("Unity Setup Fields")]
    public Transform partToRotate;
    public Transform Target { get; private set; }

    // function: Initialize
    // Purpose: Assigns parentBase to the towerBase base
    public virtual void Initialize(TowerBaseManager b)
    {
        parentBase = b;
        defaultRotation = partToRotate.rotation;
    }

    // function: Update
    // Purpose: Called every frame while the script is active.
    //          Every frame this function does a multitude of things:
    //               - Check to see if there The tower is complete, or if the head has a base
    //               - Stores information of all nearby enemies
    //               - Finds the angle where the Target can be located
    void Update()
    {
        if (parentBase == null || !parentBase.IsTowerComplete())
        {
            return;
        }

        if (parentBase.Target == null)
        {
            partToRotate.rotation = Quaternion.RotateTowards(partToRotate.rotation, defaultRotation, headData.turnSpeed * Time.deltaTime * 50f);
            return;
        }

        Vector2 dir = parentBase.Target.position - partToRotate.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float offset = -90f;
        Quaternion lookRotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));

        partToRotate.rotation = Quaternion.RotateTowards(partToRotate.rotation, lookRotation, headData.turnSpeed * Time.deltaTime * 50f);
    }

    // function: Shoot
    // Purpose: Conjures a projectilePrefab. Doing so will call the projectile's separate file to see how to track it.
    // //       The projectile will take the transform parameter as the target, and deal the float parameter's worth of damage to it.
    public void Shoot(Transform target, float damage)
    {
        if (target == null || headData == null)
        {
            return;
        }

        if (headData.projectilePrefab == null || firePoint == null)
        {
            return;
        }

        for (int i = 0; i < headData.numOfProjectiles; i++)
        {
            GameObject firedBullet = Instantiate(headData.projectilePrefab, firePoint.position, firePoint.rotation);

            BulletPhysics bullet = firedBullet.GetComponent<BulletPhysics>();
            if (bullet != null)
            {
                bullet.Seek(target, damage);
            }
        
        }
    }
}
