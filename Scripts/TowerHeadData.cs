/***************************************************************
*file: TowerBaseData.cs
*author: G. Valdiviez
*class: CS 4700 – Game Development
*assignment: Final Program
*date last modified: 11/6/2026
*
*purpose: Holds the data used in the TowerHeadManager.cs script
*         Because it is a separate file, any single shot towers can have unique stats without too much alteration
*         
*
****************************************************************/
using UnityEngine;

[CreateAssetMenu(fileName = "TowerHeadData", menuName = "Scriptable Objects/TowerHeadData")]
public class TowerHeadData : ScriptableObject
{
    [Header("Attributes")]
    public GameObject projectilePrefab;
    public int numOfProjectiles; // This is per shot, not per burst.

    [Header("Unity Setup Fields")]
    public float turnSpeed;

}
