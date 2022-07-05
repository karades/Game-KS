using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetIntervalAndPlay : MonoBehaviour
{
    [SerializeField]
    private List<bool> isIntervalOn;
    [SerializeField]
    bool isUp;
    [SerializeField]
    CustomIntervalSettingsScriptable customIntervalSettingsScriptable;

    SceneMaster sceneMaster = new SceneMaster();


    // Update is called once per frame
    void Update()
    {

    }

    void setIntervalsCustom()
    {
        for (int i = 0; i < 13; i++)
        {
            customIntervalSettingsScriptable.isInterval[i] = isIntervalOn.ElementAt(i);
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
