using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public static DamageManager instance;
    private void Awake()
    {
        instance = this;
    }
    public DamageArea SpawnDamage(string name, Damagable dealer, Vector2 position, Vector2 direction, float amount, float knockback = 1)
    {
        var dmg = Instantiate(Resources.Load<DamageArea>("Damage/" + name),
            position, Quaternion.identity, GameObject.Find("PARTICLES").transform);
        dmg.transform.up = direction.normalized;
        dmg.Initialize(amount, dealer, knockback);
        return dmg;
    }
    public Damagable Shoot(Vector2 pos1, Vector2 pos2, float damage = 1,
        float knockback = 1, string particle = "Impact", Damagable dealer = null, bool penetrates = false)
    {
        List<RaycastHit2D> Hits = new List<RaycastHit2D>();
        Physics2D.Raycast(pos1, pos2 - pos1, Config.pa.projectileMask, Hits, Vector2.Distance(pos1, pos2) + 0.5f);
        foreach (RaycastHit2D hit in Hits)
        {
            if(Damagable.Damagables.ContainsKey(hit.collider) && Damagable.Damagables[hit.collider] != dealer)
            {
                SpawnDamage(particle, dealer, hit.point, pos1- hit.point, damage, knockback);
                if(!penetrates) return Damagable.Damagables[hit.collider];
            }
        }
        return null;
    }
}
