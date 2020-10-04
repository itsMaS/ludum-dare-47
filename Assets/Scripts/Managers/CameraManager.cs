using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    public Transform target;
    public float followSpeed;

    [HideInInspector] public Camera cam;

    public Vector2 mouseWorldPosition { get { return _mouseWorldPosition; } }
    private Vector2 _mouseWorldPosition;

    private void Awake()
    {
        instance = this;
        cam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            DamageManager.instance.SpawnDamage("Circle",null, mouseWorldPosition, Vector2.up, 10);
        }
    }


    private void LateUpdate()
    {
        if (target)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position,
                (Vector2.Distance(transform.position, target.position) + 0.001f) * followSpeed*Time.deltaTime);
        }
        _mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
    }
}
