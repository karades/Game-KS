using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomIntervalMode : MonoBehaviour
{

    bool[] isIntervalOn = new bool[13];

    [SerializeField]
    Toggle[] intervalToogles;

    [SerializeField]
    CustomIntervalSettingsScriptable customIntervalSettingsScriptable;

    PlayAudio playAudio = new PlayAudio();
    SceneMaster sceneMaster = new SceneMaster();
    // Start is called before the first frame update
    void Start()
    {
        string toogleText;
        foreach (Toggle toogle in intervalToogles)
        {
           
            playAudio.IntervalsInOctave.TryGetValue(System.Int32.Parse(toogle.name), out toogleText);
            toogle.gameObject.GetComponentInChildren<Text>().text = toogleText;
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
    void setIntervalsCustom()
    {
        for(int i = 0; i < 13; i++)
        {
            customIntervalSettingsScriptable.isInterval[i] = isIntervalOn[i];
        }
  
    }
    public void goToIntervalGame()
    {
        setIntervalsCustom();
        sceneMaster.SceneLoad("SceneGuessInterval");
        //TODO SCRIPTABLE Z ISINTERVAL DO CUSTOM I DO NORMALNEGO SAAVEA
        //TODO POJSCIE DO KOLEJNEJ SCENY Z DANYMI Z SCRIPTABLE
    }
}
