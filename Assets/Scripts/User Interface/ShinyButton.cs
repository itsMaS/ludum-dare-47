using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShinyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Animator Shine;

    private RectTransform rt;
    private void Awake()
    {
        rt = Shine.GetComponent<RectTransform>();
    }
    private void Start()
    {
        //float offset = transform.position.x * 0.0025f;
        float offset = Random.value;
        Shine.SetFloat("offset", 1-offset%1);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        rt.localPosition = new Vector3(0, -5.5555555f, 0);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        rt.localPosition = new Vector3(0, 5.5555555f, 0);
    }
}
