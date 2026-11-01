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

    // Update is called once per frame
    void Update()
    {
        currAngle += speed * Time.deltaTime;

        float x = pivotPoint.x + MathF.Cos(currAngle) * radX;
        float y = pivotPoint.y + MathF.Sin(currAngle) * radY;

        transform.position = new Vector3(x, y, transform.position.z);
    }
}
