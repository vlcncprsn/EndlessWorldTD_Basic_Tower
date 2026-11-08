/***************************************************************
*file: TowerBaseData.cs
*author: G. Valdiviez
*class: CS 4700 – Game Development
*assignment: Final Program
*date last modified: 11/6/2026
*
*purpose: Holds the data used in the TowerBaseManager.cs script
*         Because it is a separate file, any single shot towers can have unique stats without too much alteration
*         
*
****************************************************************/
using UnityEngine;

[CreateAssetMenu(fileName = "TowerBaseData", menuName = "EndlessWorldsTD Tower Base Data")]
public class TowerBaseData : ScriptableObject
{
    [Header("Attributes")]
    public float health;
    public float attackRange;
    public float visionAngle = 90f;

    [Header("Unity Setup Fields")]
    public string enemyTag;
}
