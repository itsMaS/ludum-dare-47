using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    public float maxHealth;
    public float health;


    public virtual Transform pivot { get { return transform; } }
    public virtual Transform target { get { return _target != null ? _target : transform; } }
    [SerializeField] private Transform _target;

    protected Rigidbody2D rb;

    public static Dictionary<Collider2D, Damagable> Damagables = new Dictionary<Collider2D, Damagable>();

    public DamageVisual dv;
    protected virtual void Awake()
    {
        foreach (var item in GetComponentsInChildren<Collider2D>())
        {
            Damagables.Add(item, this);
        }
        dv = GetComponent<DamageVisual>();
        rb = GetComponent<Rigidbody2D>();

        health = maxHealth;
    }
    protected virtual void Update()
    {
        if (LevelManager.instance.gameOver) return;
    }
    public void TakeDamage(float amount, Vector2 source, float knockback = 1)
    {
        dv.Hit(Color.white);
        Debug.DrawLine(target.position,source);
        Debug.Log($"{gameObject.name} took {amount} of damage!");
        rb.AddForce(knockback*(source-(Vector2)target.position).normalized, ForceMode2D.Impulse);

        health -= amount;
        if(health / maxHealth <= 0.3f)
        {
            dv.Blink(Color.red, 999);
        }
        else
        {
            dv.StopBlinking();
        }
        if (health <= 0) Die();
    }
    protected virtual void Die()
    {
        StartCoroutine(PerformDie());
    }

    IEnumerator PerformDie()
    {
        AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        dv.Hit(Color.white, curve, 0.2f);
        yield return new WaitForSeconds(0.2f);
        EffectManager.instance.SpawnParticle("Explosion", pivot.position);
        Destroy(gameObject);
    }
}
