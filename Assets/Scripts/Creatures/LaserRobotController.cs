using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRobotController : EnemyController
{
    [SerializeField] private LineRenderer lr;
    [SerializeField] private Transform eye;
    [SerializeField] GameObject chargeParticles;

    private bool inRange = false;

    private Vector2 laserTarget;

    private void Start()
    {
        StartCoroutine(Shoot());
    }
    protected override void Update()
    {
        base.Update();
        if (playerTarget == null) return;
        lr.SetPosition(0, eye.position);
        lr.SetPosition(1, (Vector2)eye.position + 1000 * (laserTarget - (Vector2)eye.position).normalized);
        inRange = Vector2.Distance(eye.position, playerTarget.position) <= Config.pa.shotRange;
        lr.enabled = inRange;
        laserTarget = Vector2.MoveTowards(laserTarget, playerTarget.position, Config.pa.laserFollowSpeed * Time.deltaTime);
    }
    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(Random.Range(1, 3));
        while(!LevelManager.instance.gameOver)
        {
            if(inRange)
            {
                yield return StartCoroutine(Charge());
                if(inRange)
                {
                    //DamageManager.instance.SpawnDamage("Impact", this, 
                    //    playerTarget.position, eye.transform.position - playerTarget.position, shotDamage, shotKnockback);
                    bool hit = DamageManager.instance.Shoot(eye.position, laserTarget, Config.pa.shotDamage, Config.pa.shotKnockback, "Impact", this);
                    CameraShaker.instance.AddShake(playerTarget.position, Config.pa.shotShake);
                    StartCoroutine(FlashLaser(hit));
                    yield return new WaitForSeconds(Config.pa.shotCooldown);
                }
            }
            yield return null;
        }
    }
    IEnumerator FlashLaser(bool hit)
    {
        lr.startWidth = 0.03f;
        lr.endWidth = 0.03f;
        Color c1 = lr.startColor;
        Color c2 = lr.endColor;

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
        yield return new WaitForSeconds(Config.pa.shotChargeTime);
        chargeParticles.SetActive(false);
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(eye.position, Config.pa.shotRange);
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawLine(eye.position, eye.position + Vector3.up * Config.pa.shotRange * 0.5f);
    //    Gizmos.DrawLine(eye.position, eye.position + Vector3.up * Config.pa.shotRange * -0.5f);
    //}
}
