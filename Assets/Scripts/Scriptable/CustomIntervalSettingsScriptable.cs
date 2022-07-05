using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CustomIntervalSettingsScriptableObject", order = 1)]
public class CustomIntervalSettingsScriptable : ScriptableObject
{

    public bool[] isInterval = new bool[13];
    public bool isSelectedFromMenu = false;
    public bool isUp = true;
}
