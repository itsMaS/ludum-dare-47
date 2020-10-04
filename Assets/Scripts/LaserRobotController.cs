using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRobotController : EnemyController
{
    [SerializeField] private LineRenderer lr;
    [SerializeField] private Transform eye;

    private Vector3 target;
    private void Update()
    {
        
        lr.SetPosition(0, eye.position);
        lr.SetPosition(1, player.position + new Vector3(0,0.15f,0));
    }
}
