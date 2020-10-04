using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

[DefaultExecutionOrder(-10)]
public abstract class PlayerController : Damagable
{
    public PlayerController transformation;

    protected Vector2 direction { get { return _direction; } }
    protected Vector2 movement { get { return Vector2.Scale(direction, new Vector2(1, 0.5f)); } }

    public bool controllable = true;
    private Vector2 _direction;

    protected bool transforming = false;

    protected Animator an;
    protected override void Awake()
    {
        base.Awake();
        an = GetComponent<Animator>();
    }
    protected override void Update()
    {
        base.Update();
        _direction = controllable ? new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) : Vector2.zero;
    }
    public virtual PlayerController Transform()
    {
        transforming = true;
        controllable = false;
        var controller = Instantiate(transformation, pivot.position, Quaternion.identity).GetComponent<PlayerController>();
        return controller;
    }
    protected override void Die()
    {
        LevelManager.instance.GameOver();
        base.Die();
        controllable = false;
    }
    public override void TakeDamage(float amount, Vector2 source, float knockback = 1)
    {
        base.TakeDamage(amount, source, knockback);
        if (health / maxHealth <= 0.3f)
        {
            dv.Blink(Color.red, 999);
        }
    }
}
