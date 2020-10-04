using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransformationPanel : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    private void Start()
    {
        
    }
    private void Update()
    {
        fillImage.fillAmount = 1-LevelManager.instance.lerpRemainingTime;
        if(LevelManager.instance.lerpRemainingTime < 0.1f)
        {
            Color tmp = fillImage.color;
            tmp.a = Mathf.InverseLerp(0, 0.1f, LevelManager.instance.lerpRemainingTime);
            fillImage.color = tmp;
        }
        else
        {
            Color tmp = fillImage.color;
            tmp.a = 1;
            fillImage.color = tmp;
        }
    }
}
