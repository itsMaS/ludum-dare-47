using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : PlayerController
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpOffset;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float jumpInputThreshold;

    private bool jumping = false;

    private void Start()
    {
        StartCoroutine(Jump());
    }
    protected override void Update()
    {
        base.Update();
        if(!jumping && direction.magnitude > jumpInputThreshold)
        {
            StartCoroutine(Jump());
        }
    }
    IEnumerator Jump()
    {
        jumping = true;
        an.SetTrigger("Jump");
        yield return null;
        yield return new WaitForSeconds(jumpOffset);
        rb.AddForce(Vector2.Scale(direction.normalized * jumpForce, new Vector2(1,0.5f)), ForceMode2D.Impulse);
        yield return new WaitForSeconds(jumpCooldown - jumpOffset);
        jumping = false;
    }
    public void Land()
    {
        CameraShaker.instance.AddShake(transform.position, 5);
    }

    protected override PlayerController Transform()
    {
        throw new System.NotImplementedException();
    }
}
