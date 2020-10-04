using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraShaker : MonoBehaviour
{
    #region instance
    public static CameraShaker instance { get; internal set; }
    #endregion

    public Transform Target;

    public AnimationCurve MagnitudeCurve;
    public AnimationCurve IntensityCurve;
    public AnimationCurve DistanceCurve;

    public float shakeFalloff = 1;
    public float shakeMagnitude = 1;
    public float shakeIntensity = 1;

    private Vector3 originalPosition;

    private float shake;
    private Vector2 target;
    private Camera cam;

    private void Awake()
    {
        instance = this;
        cam = GetComponent<Camera>();
        originalPosition = transform.localPosition;
    }
    private void Update()
    {
        if(shake > 0)
        {
            shake -= shakeFalloff * Time.deltaTime;
            target = (Vector2)originalPosition + Random.insideUnitCircle * shakeMagnitude*MagnitudeCurve.Evaluate(shake);
            transform.localPosition = (Vector3)Vector2.MoveTowards(transform.localPosition,
                target, (Vector2.Distance(target, transform.localPosition)
                *shakeIntensity*IntensityCurve.Evaluate(shake))*Time.deltaTime+ 0.02f) + new Vector3(0,0,originalPosition.z);
        }
    }
    public void AddShake(Vector2 position, float force)
    {
        Target = Target ? Target : transform;
        shake = Mathf.Max(0, shake);
        shake += DistanceCurve.Evaluate(Vector2.Distance(Target.position, position))*force;
    }

    [ContextMenu("Test shake")]
    public void TestShake()
    {
        instance.AddShake(transform.position, Random.Range(0,20));
    }
}
