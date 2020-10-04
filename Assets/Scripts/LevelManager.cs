using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance { get; private set; }

    public float lerpRemainingTime { get { return Mathf.InverseLerp(0, transformationCooldown, remainingTime); } }

    public UnityEvent onTransform;
    public PlayerController active;

    public float transformationCooldown;
    public float remainingTime;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        remainingTime = transformationCooldown;
    }
    private void Update()
    {
        remainingTime -= Time.deltaTime;
        if(remainingTime <= 0)
        {
            remainingTime = transformationCooldown;
            var next = active.Transform();
            StartCoroutine(Transformation(active, next));
            active = next;
            onTransform.Invoke();
            CameraManager.instance.target = active.pivot;
        }
    }

    IEnumerator Transformation(PlayerController previous, PlayerController next)
    {
        next.gameObject.SetActive(false);
        AnimationCurve outro = AnimationCurve.EaseInOut(0, 0, 1, 1);
        previous.dv.Hit(Color.white, outro, 1);
        yield return new WaitForSeconds(1f);
        EffectManager.instance.SpawnParticle("Explosion", previous.pivot.position+new Vector3(0,0.15f,0));
        active.transform.position = previous.pivot.position;
        Destroy(previous.gameObject);
        next.gameObject.SetActive(true);
        AnimationCurve intro = AnimationCurve.EaseInOut(0, 1, 1, 0);
        next.dv.Hit(Color.white, intro, 1);
    }
}
