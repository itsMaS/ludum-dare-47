using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game config", menuName = "Config", order = 1)]
public class Config : ScriptableObject
{
    public static Config pa;

    [Header("Universal Config")]
    public float initialMaxHealth;
    public ContactFilter2D projectileMask;

    [Header("Slime Config")]
    public float jumpForce;
    public float jumpOffset;
    public float jumpCooldown;
    public float jumpInputThreshold;
    public float jumpShake;
    public float jumpDamage;

    [Header("Worm Config")]
    public float speed;
    public float jointFollowSpeed;
    public AnimationCurve distanceCurve;
    public float scaleFrequency;
    public Vector2 scaleBounds;
    public float scaleOffset;
    public AnimationCurve distanceOverChildren;

    [Header("Laser Robot config")]
    public float shotDamage = 1;
    public float shotKnockback = 2;
    public float shotShake = 2;
    public float shotRange = 3.72f;
    public float shotChargeTime = 2;
    public float shotCooldown = 0;
    public float laserFollowSpeed = 0.2f;
}