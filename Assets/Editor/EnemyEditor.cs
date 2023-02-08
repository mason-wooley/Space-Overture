using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Enemy))]
public class EnemyEditor : Editor {
    private void OnSceneGUI () {
        Enemy enemy = (Enemy)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(enemy.transform.position, Vector3.up, Vector3.forward, 360, enemy.ViewRadius);

        Vector3 viewAngleLeft = DirectionFromAngle(enemy.transform.eulerAngles.y, -enemy.ViewAngle / 2);
        Vector3 viewAngleRight = DirectionFromAngle(enemy.transform.eulerAngles.y, enemy.ViewAngle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(enemy.transform.position, enemy.transform.position + viewAngleLeft * enemy.ViewRadius);
        Handles.DrawLine(enemy.transform.position, enemy.transform.position + viewAngleRight * enemy.ViewRadius);

        if (enemy.canSeeTarget) {
            Handles.color = Color.green;
            Handles.DrawLine(enemy.transform.position, enemy.TargetObject.transform.position);
        }
    }

    private Vector3 DirectionFromAngle (float eulerY, float angleInDegrees) {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
