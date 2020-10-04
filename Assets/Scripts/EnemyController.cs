using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected Transform player { get { return LevelManager.instance.active.pivot; } }
}
