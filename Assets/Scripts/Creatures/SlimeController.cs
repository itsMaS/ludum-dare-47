using System;
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
        AudioSource source = AudioManager.Play("Slime_Jump", 0.2f);
        rb.AddForce(Vector2.Scale(tempDir * Config.pa.jumpForce.Evaluate(level), new Vector2(1,0.5f)), ForceMode2D.Impulse);
        Debug.Log($"Jump force : {Config.pa.jumpForce.Evaluate(level)}");
        yield return new WaitForSeconds(Config.pa.jumpCooldown.Evaluate(level) - Config.pa.jumpOffset);
        jumping = false;
    }
    public void Land()
    {
        AudioSource source = AudioManager.Play("Slime_Land", 0.5f);
        source.pitch = UnityEngine.Random.Range(0.6f, 1);
        CameraShaker.instance.AddShake(transform.position, Config.pa.jumpShake);
        DamageManager.instance.SpawnDamage("Circle", this, transform.position, Vector2.up, Config.pa.jumpDamage.Evaluate(level));
    }
}
