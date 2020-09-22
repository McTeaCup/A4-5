using System.Collections.Generic;
using UnityEngine;

public class GizmoDraws : MonoBehaviour
{
    [SerializeField] Transform moveset = default;
    [SerializeField] Color pathColor = Color.white;
    [HideInInspector] public List<Vector3> enemyCheckpoints;       //Checkpoints in moveset

    public void OnDrawGizmosSelected()  //Draw enemy path
    {
        for (int i = 0; i < moveset.childCount; i++)
        {
            enemyCheckpoints.Add(moveset.GetChild(i).position);
        }

        Gizmos.color = pathColor;
        for (int i = 0; i < enemyCheckpoints.Count - 1; i++)
        {
            Gizmos.DrawSphere(enemyCheckpoints[i], 0.1f);
            Gizmos.DrawLine(enemyCheckpoints[i], enemyCheckpoints[i + 1]);
        }
    }
}
