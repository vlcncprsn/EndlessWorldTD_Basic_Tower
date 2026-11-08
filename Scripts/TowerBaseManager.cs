/***************************************************************
*file: TowerBaseManager.cs
*author: G. Valdiviez
*class: CS 4700 – Game Development
*assignment: Final Program
*date last modified: 11/8/2026
*
*purpose: Allows the Towers bases to function as intended by the development team
*         Let's tower heads and cores be attached to the base, giving it a place to shoot from and a general type of damage.
*         Acts as the foundation for tower code as a whole.
*
****************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Collections.Tests.CoreCLR.TestJobs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class TowerBaseManager : MonoBehaviour
{
    [Header("Tower Base Data")]
    public TowerBaseData baseData;

    [Header("Effective Base Stats")]
    public float currentHP;
    public float currentRange;

    [Header("Unity Setup Fields")]
    public Transform socket;

    [Header("Core and Head")]
    public TowerCoreManager attachedCore;
    public TowerHeadManager attachedHead;
    public Transform Target { get; private set; }

    // function: IsTowerComplete
    // Purpose: Determines whether a base has an accompanying core and head (requirements for a functional tower)
    public bool IsTowerComplete()
    {
        return (attachedCore != null && attachedHead != null);
    }

    // function: Start
    // Purpose: Called on the frame when this script is enabled.
    //          If there are already parts on the tower, it will Initialize() them
    //          Initializes currentHP and currentRange in line with TowerBaseData.cs
    void Start()
    {
        currentHP = baseData.health;
        currentRange = baseData.attackRange;

        if (attachedCore != null)
        {
            attachedCore.Initialize(this);
        }

        if (attachedHead != null)
        {
            attachedHead.Initialize(this);
        }

        InvokeRepeating(nameof(UpdateTarget), 0f, 0.1f);
    }

    // function: UpdateTarget
    // Purpose: Every tenth of a second, this function will check to see if there is an enemy to lock on to
    //          If there isn't nearest enemy is null, if there is it determines which enemy is closest.
    //          If the tower isn't complete, then Target will be set to null until the tower is complete.
    //          If the tower already has a target, it won't switch targets until it or the tower is destroyed.
    void UpdateTarget()
    {
        if (!IsTowerComplete())
        {
            Target = null;
            return;
        }

        if (Target != null)
        {
            float distanceToTarget = Vector2.Distance(transform.position, Target.position);

            if (distanceToTarget <= currentRange)
            {
                return;
            }

            Target = null;
        }

        GameObject[] closeEnemies = GameObject.FindGameObjectsWithTag(baseData.enemyTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        Vector2 headForward = attachedHead.partToRotate.up;

        foreach (GameObject enemy in closeEnemies)
        {
            Vector2 directionToEnemy = enemy.transform.position - transform.position;
            float distanceToEnemy = directionToEnemy.magnitude;

            if (distanceToEnemy > currentRange)
            {
                continue;
            }

            float angleToEnemy = Vector2.Angle(headForward, directionToEnemy);

            if (angleToEnemy > baseData.visionAngle / 2f)
            {
                continue;
            }

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        Target = nearestEnemy != null ? nearestEnemy.transform : null;

    }

    // function: Update
    // Purpose: Called every frame while the script is active.
    //          Right Now, it has a damage key in it, since as of 11/8/2026, I haven't tested towers against enemy AI yet.
    void Update()
    {
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            TakeDamage(10f);
        }
    }

    // function: TakeDamage
    // Purpose: If an enemy collides with the tower, it will decrease its health by the variable damage.
    //          If the tower's health reaches zero, all components of the tower (Base, Core, Head) are destroyed.
    public void TakeDamage(float damage)
    {
        currentHP -= damage;


        if (currentHP <= 0f) 
        {
            if (attachedCore != null)
            {
                Destroy(attachedCore.gameObject);

            }

            if (attachedHead != null)
            {
                Destroy(attachedHead.gameObject);

            }

            Destroy(gameObject);
        }

    }


    // function: AttachCore
    // Purpose: Attaches a core to the base. This isn't fully implement yet.
    public void AttachCore(TowerCoreManager core)
    {
        if (core == null)
        {
            return;
        }

        if (attachedCore != null)
        {
            return;
        }

        core.transform.SetParent(transform, false);
        core.transform.localPosition = socket.localPosition;
        core.transform.localRotation = Quaternion.identity;

        attachedCore = core;
        core.Initialize(this);

        return;

    }

    // function: AttachHead
    // Purpose: Attaches a head to the base. This isn't fully implement yet.
    public void AttachHead(TowerHeadManager head)
    {
        if (head == null)
        {
            return;
        }

        if (attachedHead != null)
        {
            return;
        }

        head.transform.SetParent(transform, false);
        head.transform.localPosition = socket.localPosition;
        head.transform.localRotation = Quaternion.identity;

        attachedHead = head;
        head.Initialize(this);

        return;

    }
}
