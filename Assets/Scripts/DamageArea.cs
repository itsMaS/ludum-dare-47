using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    List<Damagable> AlreadyHit = new List<Damagable>();
    List<Damagable> Excluded = new List<Damagable>();

    private float damage;
    private float knockback;

    [SerializeField] private Transform source;

    private void Awake()
    {
        source = source ? source : transform;
    }
    public void Die()
    {
        Destroy(gameObject);
    }

    public void Initialize(float damage, Damagable[] Excluded, float knockback = 1)
    {
        this.knockback = knockback;
        this.Excluded = new List<Damagable>(Excluded);
    }
    public void Initialize(float damage, Damagable Dealer, float knockback = 1)
    {
        this.damage = damage;
        this.knockback = knockback;
        Excluded = new List<Damagable>();
        if(Dealer)
        Excluded.Add(Dealer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Damagable.Damagables.ContainsKey(collision) && 
            !AlreadyHit.Contains(Damagable.Damagables[collision]) &&
            !Excluded.Contains(Damagable.Damagables[collision]))
        {
            Damagable.Damagables[collision].TakeDamage(damage, source.position, knockback);
        }
    }
}
