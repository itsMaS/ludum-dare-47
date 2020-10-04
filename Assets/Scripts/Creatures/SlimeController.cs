﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : PlayerController
{
    private bool jumping = false;

    protected override void Update()
    {
        base.Update();
        if(!jumping && direction.magnitude > Config.pa.jumpInputThreshold)
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
        yield return new WaitForSeconds(Config.pa.jumpOffset);
        rb.AddForce(Vector2.Scale(tempDir * Config.pa.jumpForce, new Vector2(1,0.5f)), ForceMode2D.Impulse);
        yield return new WaitForSeconds(Config.pa.jumpCooldown - Config.pa.jumpOffset);
        jumping = false;
    }
    public void Land()
    {
        CameraShaker.instance.AddShake(transform.position, Config.pa.jumpShake);
        DamageManager.instance.SpawnDamage("Circle", this, transform.position, Vector2.up, Config.pa.jumpDamage);
    }
}
