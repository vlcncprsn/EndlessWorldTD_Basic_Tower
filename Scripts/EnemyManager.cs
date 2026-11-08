/***************************************************************
*file: EnemyManager.cs
*author: G. Valdiviez
*class: CS 4700 – Game Development
*assignment: Final Program
*date last modified: 11/6/2026
*
*purpose: Extremely rudimentary enemy AI. Just moves the enemy in an ellipse.
*
****************************************************************/

using System;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Vector3 pivotPoint = Vector3.zero;
    private float speed = 0.5f;

    [Header("Ellipse Dimensions")]
    public float radX = 15f;
    public float radY = 3f;

    private float currAngle = 0f;

    // function: Update
    // Purpose: repositions the enemy based on its current angle, where the pivot point is, and the radius of the ellipse.
    void Update()
    {
        currAngle += speed * Time.deltaTime;

        float x = pivotPoint.x + MathF.Cos(currAngle) * radX;
        float y = pivotPoint.y + MathF.Sin(currAngle) * radY;

        transform.position = new Vector3(x, y, transform.position.z);
    }
}
