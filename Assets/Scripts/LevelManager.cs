using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance { get; private set; }

    public UnityEvent onTransform;

    public PlayerController active;

    public float FlipTime;

    private float remainingTime;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        remainingTime = FlipTime;
        StartCoroutine(SwichCounter());
    }
    IEnumerator SwichCounter()
    {
        while(true)
        {

        }
    }
    private void Switch()
    {

    }
}
