using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class XPManager : MonoBehaviour
{
    [SerializeField] private Transform worldCanvas;
    [SerializeField] private GameObject xpPopup;
    [SerializeField] private GameObject levelUpPopup;

    public float progressNormalized { get { return Mathf.InverseLerp(previousXP, requiredXP, currentXP); } }
    public int previousXP { get { return Mathf.CeilToInt(ProgressionCurve.Evaluate(level)); } }
    public int requiredXP { get { return Mathf.CeilToInt(ProgressionCurve.Evaluate(level+1)); } }

    public AnimationCurve ProgressionCurve;

    public int currentXP;
    public int level = 1;

    public static XPManager instance;
    private void Awake()
    {
        instance = this;
    }
    public void GainXP(int amount)
    {
        Debug.Log($"Gained XP {amount}");

        currentXP += amount;
        if(currentXP >= requiredXP)
        {
            level++;
            LevelUp();
        }
        else
        {
            var tmp = Instantiate(xpPopup,LevelManager.instance.active.target.position,
                Quaternion.identity, worldCanvas);
            tmp.GetComponentInChildren<TextMeshProUGUI>().SetText($"+{amount} XP");
            Destroy(tmp, 3);
        }
    }
    private void LevelUp()
    {
        var tmp = Instantiate(levelUpPopup, LevelManager.instance.active.target.position,
            Quaternion.identity, worldCanvas);
        Destroy(tmp, 3);
        Debug.Log("Level up");
    }
}
