using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormController : PlayerController
{
    [SerializeField] private Transform _pivot;

    [SerializeField] private Transform[] Parts;

    protected override void Awake()
    {
        base.Awake();
        rb = Parts[0].GetComponent<Rigidbody2D>();
    }

    protected override void Update()
    {
        base.Update();
        rb.AddForce(movement*Config.pa.speed*Time.deltaTime);
        Parts[0].transform.localScale = Vector3.one *
                Mathf.Lerp(Config.pa.scaleBounds.x, Config.pa.scaleBounds.y, Mathf.InverseLerp
                (-2, 1, Mathf.Sin(Time.time * Config.pa.scaleFrequency)));
        for (int i = 1; i < Parts.Length; i++)
        {
            Parts[i].transform.localScale = Vector3.one * 
                Mathf.Lerp(Config.pa.scaleBounds.x, Config.pa.scaleBounds.y,Mathf.InverseLerp
                (-2, 1, Mathf.Sin(Config.pa.scaleOffset * i + Time.time * Config.pa.scaleFrequency)));
            float speed = Config.pa.distanceOverChildren.Evaluate(i)*Config.pa.distanceCurve.
                Evaluate(Vector2.Distance(Parts[i].position, Parts[i - 1].position));
            Parts[i].transform.position = Vector2.MoveTowards(Parts[i].position, Parts[i - 1].position, speed);
        }
    }
    public override Transform pivot => _pivot;
}
