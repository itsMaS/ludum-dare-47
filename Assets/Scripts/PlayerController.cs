using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class PlayerController : MonoBehaviour
{
    public PlayerController transformation;

    private Vector2 _direction;
    protected Vector2 direction { get { return _direction; } }
    protected Vector2 movement { get { return Vector2.Scale(direction, new Vector2(1, 0.5f)); } }


    protected Rigidbody2D rb;
    protected Animator an;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
    }
    protected virtual void Update()
    {
        _direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    protected abstract PlayerController Transform();
}
