using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SaveData", menuName = "ScriptableObjects/SaveData")]
public class SaveDataScriptable : ScriptableObject
{
    public string Name = "MusicMan";
    public Dictionary<string, bool> unlocked = new Dictionary<string, bool>()
    {
        {"1",false},
        {"2>",false},
        {"2",false},
        {"3>",false},
        {"3",false},
        {"4",false},
        {"4<",false},
        {"5",false},
        {"6>",false},
        {"6",false},
        {"7",false},
        {"7<",false},
        {"8",false},
        {"major0",false},
        {"major1",false},
        {"major2",false},
        {"minor0",false},
        {"minor1",false},
        {"minor2",false},
        {"D7",false },
        {"D65",false},
        {"D43",false },
        {"D2",false}
    };

}
