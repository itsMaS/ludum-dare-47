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
    public Vector2Int XPfromHeal;

    [Header("Slime Config")]
    public AnimationCurve jumpForce;
    public AnimationCurve jumpCooldown;
    public AnimationCurve jumpDamage;
    public float jumpOffset;
    public float jumpShake;
    public float jumpInputThreshold;

    [Header("Worm Config")]
    public AnimationCurve speed;
    public float jointFollowSpeed;
    public AnimationCurve distanceCurve;
    public float scaleFrequency;
    public Vector2 scaleBounds;
    public float scaleOffset;
    public AnimationCurve distanceOverChildren;
    public float healDistance;
}