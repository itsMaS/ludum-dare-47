﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRobotController : EnemyController
{
    [Header("Parameters")]
    public float shotDamage = 1;
    public float shotKnockback = 2;
    public float shotShake = 2;
    public float shotRange = 3.72f;
    public float shotChargeTime = 2;
    public float shotCooldown = 0;
    public float laserRobotHealth = 5f;

    [Header("Dependancies")]
    [SerializeField] private LineRenderer lr;
    [SerializeField] private Transform eye;
    [SerializeField] GameObject chargeParticles;

    private bool inRange = false;

    private Vector2 laserTarget;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Shoot());
        maxHealth = laserRobotHealth;
        health = laserRobotHealth;
    }
    protected override void Update()
    {
        base.Update();
        if (playerTarget == null) return;
        lr.SetPosition(0, eye.position);
        inRange = Vector2.Distance(eye.position, playerTarget.position) <= shotRange;
        lr.enabled = inRange;
        if(!used) lr.SetPosition(1, playerTarget.position);
    }


    bool used = false;
    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(Random.Range(1, 3));
        while(!LevelManager.instance.gameOver)
        {
            if(inRange)
            {
                Vector2 direction = (Vector2)playerTarget.position - (Vector2)eye.position;
                lr.SetPosition(1, (Vector2)eye.position+direction.normalized*100);
                used = true;
                yield return StartCoroutine(Charge());
                bool hit = DamageManager.instance.Shoot(eye.position, direction, shotRange, shotDamage, shotKnockback, "Impact", this);
                CameraShaker.instance.AddShake(playerTarget.position, shotShake);
                yield return StartCoroutine(FlashLaser(hit));
                used = false;
                yield return new WaitForSeconds(shotCooldown);
            }
            lr.SetPosition(1, playerTarget.position);
            yield return null;
        }
    }
    IEnumerator FlashLaser(bool hit)
    {
        lr.startWidth = 0.03f;
        lr.endWidth = 0.03f;
        Color c1 = lr.startColor;
        Color c2 = lr.endColor;

        AudioManager.Play("Laser");
        lr.startColor = Color.white;
        lr.endColor = Color.white;
        yield return new WaitForSeconds(0.1f);

        lr.startColor = c1;
        lr.endColor = c2;
        lr.startWidth = 0.02f;
        lr.endWidth = 0.02f;
    }
    IEnumerator Charge()
    {
        chargeParticles.SetActive(true);
        if(!inRange)
        {
            chargeParticles.SetActive(false);
            yield break;
        }
        yield return new WaitForSeconds(shotChargeTime);
        chargeParticles.SetActive(false);
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(eye.position, shotRange);
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawLine(eye.position, eye.position + Vector3.up * shotRange * 0.5f);
    //    Gizmos.DrawLine(eye.position, eye.position + Vector3.up * shotRange * -0.5f);
    //}
}
