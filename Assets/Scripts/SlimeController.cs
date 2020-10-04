using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : PlayerController
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpOffset;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float jumpInputThreshold;
    [SerializeField] private float jumpShake;
    private bool jumping = false;

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
        Vector2 tempDir = direction.normalized;
        yield return null;
        yield return new WaitForSeconds(jumpOffset);
        rb.AddForce(Vector2.Scale(tempDir * jumpForce, new Vector2(1,0.5f)), ForceMode2D.Impulse);
        yield return new WaitForSeconds(jumpCooldown - jumpOffset);
        jumping = false;
    }
    public void Land()
    {
        CameraShaker.instance.AddShake(transform.position, jumpShake);
    }
}
