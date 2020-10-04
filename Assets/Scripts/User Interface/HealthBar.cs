using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;

    private void Update()
    {
        healthBar.value = LevelManager.instance.playerHealthNormalized;
    }
}
