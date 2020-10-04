using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public static EnemyHealthManager instance;
    private void Awake()
    {
        instance = this;
    }
    public Canvas healthCanvas;
    public GameObject healthBar;

    public HealthBar AddHealthBar(Damagable enemy)
    {
        var bar = Instantiate(healthBar,enemy.pivot.position,Quaternion.identity, healthCanvas.transform).GetComponent<HealthBar>();
        bar.tracked = enemy;
        bar.player = false;
        return bar;
    }
}
