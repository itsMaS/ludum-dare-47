using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVisual : MonoBehaviour
{
    public Shader overlayShader;
    public AnimationCurve hitCurve;

    List<SpriteRenderer> Renderers;
    List<Material> Materials;
    private void Awake()
    {
        Renderers = new List<SpriteRenderer>();
        Materials = new List<Material>();
        GetComponentsInChildren<SpriteRenderer>(Renderers);
        foreach (var item in Renderers)
        {
            if (item.material.shader == overlayShader) Materials.Add(item.material);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Blink(Color.red, 2, 0.5f, 0.5f);
            //Hit();
        }
    }
    private Coroutine hitCoroutine;

    [ContextMenu("Test hit")]
    public void Hit(Color color, AnimationCurve curve = null, float duration = 0.2f)
    {
        if (hitCoroutine != null) StopCoroutine(hitCoroutine);
        hitCoroutine = StartCoroutine(PerformHit(duration, color, curve));
    }

    Coroutine blinkCoroutine;

    [ContextMenu("Blink")]
    public void Blink(Color color, float duration = 2f, float blinkDuration = 0.5f, float blinkOpacity = 0.2f)
    {
        AnimationCurve pulseCurve = AnimationCurve.EaseInOut(0, 0, 1, 0);
        pulseCurve.AddKey(0.5f, blinkOpacity);
        if (blinkCoroutine != null) StopCoroutine(blinkCoroutine);
        blinkCoroutine = StartCoroutine(PerformBlinking(pulseCurve, color, duration, blinkDuration));
    }
    public void StopBlinking()
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            if(hitCoroutine == null)
            {
                foreach (var item in Materials)
                {
                    item.SetFloat("_OverlayLerp", 0);
                }
            }
        }
    }
    IEnumerator PerformBlinking(AnimationCurve curve, Color color, float duration = 2f, float blinkDuration = 0.5f)
    {
        float time = Time.time + duration;
        while(time > Time.time)
        {
            yield return StartCoroutine(PerformHit(blinkDuration, color, curve));
        }
    }
    IEnumerator PerformHit(float duration, Color color, AnimationCurve curve = null)
    {
        if (curve == null) curve = hitCurve;
        float lerp = 0;
        while(lerp < 1)
        {
            foreach (var item in Materials)
            {
                item.SetColor("_OverlayColor", color);
                item.SetFloat("_OverlayLerp", curve.Evaluate(lerp));
            }
            lerp += Time.deltaTime / duration;
            yield return null;
        }
        foreach (var item in Materials)
        {
                item.SetFloat("_OverlayLerp", 0);
        }
    }
}
