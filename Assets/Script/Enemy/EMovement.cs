using System.Collections.Generic;
using UnityEngine;

public class EMovement : MonoBehaviour
{
    public EnemyProperties enemyProperties;
    Transform movePontsParent = default;                   //Object with all movement points collected
    Transform moveset;                                    //Specific moveset
    [HideInInspector] public List<Vector3> enemyCheckpoints;       //Checkpoints in moveset

    int _currentCheckpoint = 0;
    Transform transformChild = default;

    private void Awake()
    {
        Rigidbody2D _objectRigedbody = default;
        movePontsParent = GameObject.Find("EnemySpawner").transform;
        _objectRigedbody = GetComponent<Rigidbody2D>();
        _objectRigedbody.gravityScale = 0f;
        transformChild = transform.GetChild(0);
        
        GetEnemyCheckpoints();
    }

    private void Update()
    {
        //Makes the enemy aproch points
        AprochStyle();
    }

    void GetEnemyCheckpoints()  //Finds all checkpoints
    {
        switch (enemyProperties.movementType)
        {
            case MovementType.SideToSide:
                {
                    moveset = movePontsParent.GetChild(0);
                    break;
                }

            case MovementType.WShape:
                {
                    moveset = movePontsParent.GetChild(1);
                    break;
                }

            case MovementType.HourGlass:
                {
                    moveset = movePontsParent.GetChild(2);
                    break;
                }

            case MovementType.Circle:
                {
                    moveset = movePontsParent.GetChild(3);
                    break;
                }

            case MovementType.Swarm:
                {
                    moveset = movePontsParent.GetChild(4);
                    break;
                }
        }

        //Adds all points to a list
        for (int i = 0; i < moveset.childCount; i++)
        {
            enemyCheckpoints.Add(moveset.GetChild(i).position);
        }
    }

    void AprochStyle()  //Applies aprochstyle
    {
        switch (enemyProperties.aprochStyle)
        {
            case ApprochStyle.Standard:
                {
                    transform.position = Vector3.MoveTowards(transform.position, 
                        enemyCheckpoints[_currentCheckpoint], enemyProperties.movementSpeed * Time.deltaTime);
                    break;
                }

            case ApprochStyle.Slerp:
                {
                    transform.position = Vector3.Slerp(transform.position, 
                        enemyCheckpoints[_currentCheckpoint], enemyProperties.movementSpeed * Time.deltaTime);
                    break;
                }

            case ApprochStyle.Lerp:
                {
                    transform.position = Vector3.Lerp(transform.position, 
                        enemyCheckpoints[_currentCheckpoint], enemyProperties.movementSpeed * Time.deltaTime);
                    break;
                }

            case ApprochStyle.Wave:
                {
                    transform.position = Vector3.MoveTowards(transform.position,
                        enemyCheckpoints[_currentCheckpoint], enemyProperties.movementSpeed * Time.deltaTime);

                    transformChild.position = new Vector2(transformChild.position.x,
                        (Mathf.Sin(Time.time * enemyProperties.frequency) * enemyProperties.hight) + transform.position.y);
                    break;
                }
        }

        MoveMultiblePoints();
    }

    void MoveMultiblePoints()   //Chooses point to aproch
    {
        for (int i = 0; i < enemyCheckpoints.Count; i++)
        {
            //If enemy is within 0.1 units from checkpoint, find a new point
            if (Vector2.Distance(transform.position, enemyCheckpoints[_currentCheckpoint]) < 0.1f)
            {
                //Find next point in list
                if (i == _currentCheckpoint)
                {
                    _currentCheckpoint++;
                }

                //If last point in list, go to first point
                else if (_currentCheckpoint == enemyCheckpoints.Count - 1)
                {
                    _currentCheckpoint = 0;
                }
            }
        }
    }
}
