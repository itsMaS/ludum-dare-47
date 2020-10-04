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
}