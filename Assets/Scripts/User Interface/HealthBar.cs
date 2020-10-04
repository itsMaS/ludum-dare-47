using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    public Damagable tracked;

    public bool player = true;

    private void Start()
    {
        if(player) { tracked = LevelManager.instance.active; }
    }
    private void Update()
    {
        healthBar.value = tracked.healthNormalized;
        if (!player && tracked) transform.position = (Vector2)tracked.pivot.position + Vector2.up * 1f;
        else {tracked = tracked = LevelManager.instance.active; }
    }
}
