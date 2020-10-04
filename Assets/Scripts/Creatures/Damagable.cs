using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    public UnityEvent onDeath;

    public float maxHealth;
    public float health;

    public virtual Transform pivot { get { return transform; } }
    public virtual Transform target { get { return _target != null ? _target : transform; } }
    public float healthNormalized { get { return Mathf.InverseLerp(0, maxHealth, health); } }
    [SerializeField] private Transform _target;

    protected Rigidbody2D rb;

    public static Dictionary<Collider2D, Damagable> Damagables = new Dictionary<Collider2D, Damagable>();

    protected int level { get { return XPManager.instance.level; } }

    public DamageVisual dv;
    protected virtual void Awake()
    {
        foreach (var item in GetComponentsInChildren<Collider2D>())
        {
            Damagables.Add(item, this);
        }
        dv = GetComponent<DamageVisual>();
        rb = GetComponent<Rigidbody2D>();
    }
    protected virtual void Update()
    {
        if (LevelManager.instance.gameOver) return;
    }
    public virtual void TakeDamage(float amount, Vector2 source, float knockback = 1)
    {
        AudioManager.Play("Damage").pitch = Random.Range(0.6f,1);
        dv.Hit(Color.white);
        Debug.DrawLine(target.position,source);
        Debug.Log($"{gameObject.name} took {amount} of damage!");
        rb.AddForce(knockback*(source-(Vector2)target.position).normalized, ForceMode2D.Impulse);

        health -= amount;
        if (health <= 0) Die();
    }
    protected virtual void Die()
    {
        AudioManager.Play("Death", 0.8f);
        StartCoroutine(PerformDie());
        onDeath?.Invoke();
    }

    IEnumerator PerformDie()
    {
        AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        dv.Hit(Color.white, curve, 0.2f);
        yield return new WaitForSeconds(0.2f);
        EffectManager.instance.SpawnParticle("Explosion", pivot.position);
        Destroy(gameObject);
    }

    public void RemoveFromCache()
    {
        foreach (var item in GetComponentsInChildren<Collider2D>())
        {
            Damagables.Remove(item);
        }
    }
    protected virtual void OnDestroy()
    {
        RemoveFromCache();
    }
    public void Heal(float amount)
    {
        health = Mathf.Max(health + amount, maxHealth);
        if(healthNormalized > 0.3f)
        {
            dv.StopBlinking();
        }
    }
}
