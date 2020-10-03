using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform target;
    public float followSpeed;

    [HideInInspector] public Camera cam;

    public Vector2 mouseWorldPosition { get { return _mouseWorldPosition; } }
    private Vector2 _mouseWorldPosition;

    public static CameraManager instance;
    private void Awake()
    {
        instance = this;
        cam = GetComponentInChildren<Camera>();
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
