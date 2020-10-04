using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    float healAmount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Damagable.Damagables.ContainsKey(collision) && Damagable.Damagables[collision] is PlayerController)
        {
            Damagable.Damagables[collision].Heal(healAmount);
        }
    }
}
