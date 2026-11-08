/***************************************************************
*file: TowerBaseData.cs
*author: G. Valdiviez
*class: CS 4700 – Game Development
*assignment: Final Program
*date last modified: 11/6/2026
*
*purpose: Holds the data used in the TowerCoreManager.cs script
*         Because it is a separate file, any single shot towers can have unique stats without too much alteration
*         
*
****************************************************************/
using UnityEngine;

[CreateAssetMenu(fileName = "TowerCoreData", menuName = "Scriptable Objects/TowerCoreData")]
public class TowerCoreData : ScriptableObject
{
    [Header("Attributes")]
    public float damage;
    public float fireInterval;
    public int shotsPerBurst;

    [Header("Unity Setup Field")]
    public Transform areaOfEffect;
}
