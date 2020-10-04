using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Damagable
{
    public Vector2Int XPWhenKilled;

    protected Transform playerPosition { get { return LevelManager.instance.active ? LevelManager.instance.active.pivot : null; } }
    protected Transform playerTarget { get { return LevelManager.instance.active ? LevelManager.instance.active.target : null; } }

    HealthBar healthBar;

    protected virtual void Start()
    {
        healthBar = EnemyHealthManager.instance.AddHealthBar(this);
    }
    protected override void Die()
    {
        XPManager.instance.GainXP(Random.Range(XPWhenKilled.x, XPWhenKilled.y));
        base.Die();
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        if(healthBar)
        Destroy(healthBar.gameObject);
    }
}
