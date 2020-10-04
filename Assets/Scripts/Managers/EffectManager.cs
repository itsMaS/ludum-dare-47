using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;
    private void Awake()
    {
        instance = this;
    }
    public ParticleSystem SpawnParticle(string name, Vector2 position)
    {
        return Instantiate(Resources.Load<ParticleSystem>("Effects/" + name),
            position, Quaternion.identity, GameObject.Find("PARTICLES").transform);
    }
}
