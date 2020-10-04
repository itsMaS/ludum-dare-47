using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EXBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Level;
    [SerializeField] private TextMeshProUGUI Experience;

    private void Update()
    {
        Level.SetText($"Level {XPManager.instance.level.ToString()}");
        Experience.SetText($"XP {XPManager.instance.currentXP}/{XPManager.instance.requiredXP}");
    }
}
