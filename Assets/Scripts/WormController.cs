using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormController : PlayerController
{
    [SerializeField] private float speed;
    [SerializeField] private float jointFollowSpeed;
    [SerializeField] private AnimationCurve distanceCurve;

    [SerializeField] private float scaleFrequency;
    [SerializeField] private Vector2 scaleBounds;
    [SerializeField] private float scaleOffset;
    [SerializeField] private Transform _pivot;

    [SerializeField] private Transform[] Parts;

    private Vector2 target;
    private float distance;

    protected override void Awake()
    {
        base.Awake();
        rb = Parts[00].GetComponent<Rigidbody2D>();
    }

    protected override void Update()
    {
        base.Update();
        rb.AddForce(movement*speed*Time.deltaTime);
        distance += rb.velocity.magnitude;
        Parts[0].transform.localScale = Vector3.one *
                Mathf.Lerp(scaleBounds.x, scaleBounds.y, Mathf.InverseLerp
                (-2, 1, Mathf.Sin(Time.time * scaleFrequency)));
        for (int i = 1; i < Parts.Length; i++)
        {
            Parts[i].transform.localScale = Vector3.one * 
                Mathf.Lerp(scaleBounds.x,scaleBounds.y,Mathf.InverseLerp
                (-2, 1, Mathf.Sin(scaleOffset * i + Time.time * scaleFrequency)));
            float speed = distanceCurve.Evaluate(Vector2.Distance(Parts[i].position, Parts[i - 1].position));
            Parts[i].transform.position = Vector2.MoveTowards(Parts[i].position, Parts[i - 1].position, speed);
        }
    }
    public override Transform pivot => _pivot;
}
