/***************************************************************
*file: TowerCoreManager.cs
*author: G. Valdiviez
*class: CS 4700 – Game Development
*assignment: Final Program
*date last modified: 11/6/2026
*
*purpose: Allows the Tower Cores to function as intended by the development team
*         Determines the physics attributes of the tower, i.e. damage, fire rate, shots per burst.
*         Can be attached to a TowerBase
*
****************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Collections.Tests.CoreCLR.TestJobs;
using Unity.VisualScripting;
using UnityEngine;

public class TowerCoreManager : MonoBehaviour
{
    [Header("Core Data")]
    public TowerCoreData coreData;
    protected TowerBaseManager parentBase;

    [Header("Effective Tower Stats")]
    public float currDamage;
    public float currFireInterval;
    public int currShotsPerBurst;
    private float fireCountdown = 0f;

    // function: Initialize
    // Purpose: The data from coreData is implemented in this script, as well as parentBase being assigned to the base b.
    public virtual void Initialize(TowerBaseManager b)
    {
        parentBase = b;

        currDamage = coreData.damage;

        if (coreData == null)
        {
            return;
        }

        currDamage = coreData.damage;
        currFireInterval = coreData.fireInterval;
        currShotsPerBurst = coreData.shotsPerBurst;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // function: Update
    // Purpose: Called every frame while the script is active.
    //          Every frame this function does a multitude of things:
    //               - Check to see if there The tower is complete, or if the core has a base
    //               - Checks to see if the tower is able to be fired
    //               - If it is, then it calls FireBurst() and restarts the fireCountdown.
    void Update()
    {
        if (parentBase == null || !parentBase.IsTowerComplete())
        {
            return;
        }

        if (parentBase.Target == null)
        {
            return;
        }

        if (fireCountdown <= 0f)
        {
            StartCoroutine(FireBurst());
            fireCountdown = MathF.Max(0.01f, currFireInterval);
        }

        fireCountdown -= Time.deltaTime;
    }

    // function: FireBurst()
    // Purpose: uses TowerData.shotsPerBurst to call Shoot that many times within 0.1f second intervals.
    //          If an enemy were to leave the tower's range before the burst finishes, the remaining bullets won't spawn in.
    IEnumerator FireBurst()
    {
        
        for (int i = 0; i < currShotsPerBurst; i++)
        {
            

            if (parentBase == null || !parentBase.IsTowerComplete())
            {
                yield break;
            }

            if (parentBase.Target == null)
            {
                yield break;
            }

            if (parentBase.attachedHead == null)
            {
                yield break;
            }
            
            parentBase.attachedHead.Shoot(parentBase.Target, currDamage);
            yield return new WaitForSeconds(0.1f);
        }
    }

}
