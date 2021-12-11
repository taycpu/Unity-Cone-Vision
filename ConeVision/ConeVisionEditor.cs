using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ConeVision))]
public class ConeVisionEditor : Editor
{
    private void OnSceneGUI()
    {
        ConeVision enemy = (ConeVision) target;
        Handles.color = Color.white;
        Handles.DrawWireArc(enemy.transform.position, Vector3.up, Vector3.forward, 360, enemy.radius);

        Vector3 viewAngle01 = DirectionFromAngle(enemy.transform.eulerAngles.y, -enemy.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(enemy.transform.eulerAngles.y, enemy.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(enemy.transform.position, enemy.transform.position + viewAngle01 * enemy.radius);
        Handles.DrawLine(enemy.transform.position, enemy.transform.position + viewAngle02 * enemy.radius);
        if (enemy.canSeePlayer)
        {
            Handles.color = Color.green;
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}