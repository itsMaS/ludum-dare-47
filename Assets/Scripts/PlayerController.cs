using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class PlayerController : MonoBehaviour
{
    public PlayerController transformation;

    protected Vector2 direction { get { return _direction; } }
    protected Vector2 movement { get { return Vector2.Scale(direction, new Vector2(1, 0.5f)); } }

    public bool controllable = true;

    private Vector2 _direction;
    internal DamageVisual dv;
    protected Rigidbody2D rb;
    protected Animator an;
    protected virtual void Awake()
    {
        dv = GetComponent<DamageVisual>();
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
    }
    protected virtual void Update()
    {
        _direction = controllable ? new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) : Vector2.zero;
    }
    public virtual PlayerController Transform()
    {
        controllable = false;
        var controller = Instantiate(transformation, pivot.position, Quaternion.identity).GetComponent<PlayerController>();
        return controller;
    }
    public virtual Transform pivot { get { return transform; } }
}
