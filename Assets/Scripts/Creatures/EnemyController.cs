using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Damagable
{
    protected Transform playerPosition { get { return LevelManager.instance.active ? LevelManager.instance.active.pivot : null; } }
    protected Transform playerTarget { get { return LevelManager.instance.active ? LevelManager.instance.active.target : null; } }
}
