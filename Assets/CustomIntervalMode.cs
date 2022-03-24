using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomIntervalMode : MonoBehaviour
{

    bool[] isIntervalOn = new bool[13];

    [SerializeField]
    Toggle[] intervalToogles;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Toggle toogle in intervalToogles)
        {
            toogle.gameObject.GetComponentInChildren<Text>().text = toogle.name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool[] getIntervalsCustom()
    {
        bool[] isInterval= new bool[13];
        int i = 0;
        foreach (Toggle toogle in intervalToogles)
        {
            isIntervalOn[i] = toogle.isOn;
            i++;
        }

        return isInterval;

    }

    public void goToIntervalGame()
    {
        //TODO SCRIPTABLE Z ISINTERVAL DO CUSTOM I DO NORMALNEGO SAAVEA
        //TODO POJSCIE DO KOLEJNEJ SCENY Z DANYMI Z SCRIPTABLE
    }
}
