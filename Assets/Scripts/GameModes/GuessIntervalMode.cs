using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.SmartFormat.Extensions;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.UI;

public class GuessIntervalMode : MonoBehaviour
{
    [SerializeField]
    Button button1;

    [SerializeField]
    Button button2;

    [SerializeField]
    Button playIntervalButton;

    [SerializeField]
    PlayAudio playAudio;

    [SerializeField]
    Text resultText;

    [SerializeField]
    Text HPText;
    [SerializeField]
    CustomIntervalSettingsScriptable customIntervalSettingsScriptable;



    [SerializeField]
    GameObject gameOver;
    [SerializeField]
    GameObject game;

    public int result = 0;

    [SerializeField]
    int HP = 3;

    int resultInterval;

    private int intervalsToWin = 5;

    string wrongIntervalText;
    string intervalText;

    //mo¿e nie tutaj, na test kwarty i kwinty, zostawione do deva
    bool[] isInterval = new bool[] { true, false, false, false, false, true, false, true, false, false, false, false, true };

    List<int> intervalsToGuess = new List<int>();

    [SerializeField]
    bool isStableNote = true;

    [SerializeField]
    bool isDevMode = true;

    bool isUp;

    void Start()
    {
        isUp = customIntervalSettingsScriptable.isUp;
        setResultText();
        setHPText();
        setPlayIntervalButton();
        //checkIFgonext
        changeButtons();

    }



    // Update is called once per frame
    void Update()
    {
        
    }

    void setRandomInterval()
    {
        resultInterval = UnityEngine.Random.Range(1,13);
    }

    List<int> getIntervalsList()
    {
        intervalsToGuess.Clear();
        bool isAnyInterval = false;
        if (isDevMode)
        { 
            for (int i = 0; i < isInterval.Length; i++)
            {
                if (isInterval[i])
                {
                    intervalsToGuess.Add(i + 1);
                    isAnyInterval = true;
                }
            }
        }
        else
        {

            for (int i = 0; i < customIntervalSettingsScriptable.isInterval.Length; i++)
            {
                
                if (customIntervalSettingsScriptable.isInterval[i])
                {
                    intervalsToGuess.Add(i + 1);
                    isAnyInterval = true;
                }
            }
        }
        if (!isAnyInterval)
        {
            Debug.LogError("No intervals selected, used from devmode" + this);
            for (int i = 0; i < isInterval.Length; i++)
            {
                if (isInterval[i])
                {
                    intervalsToGuess.Add(i + 1);
                    isAnyInterval = true;
                }
            }
        }
        
        return intervalsToGuess;
    }
    void setSpecificIntervals(bool[] isIntervalsF)
    {   
        resultInterval = getIntervalsList()[UnityEngine.Random.Range(0, intervalsToGuess.Count)];
    }
    void changeButtons()
    {
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();

        setResultText();

        //TODO coœ z savem by dzia³a³o
        if (customIntervalSettingsScriptable && !isDevMode) { 
            setSpecificIntervals(customIntervalSettingsScriptable.isInterval); 
        }
        else
        {
            setSpecificIntervals(isInterval);
        }

        if (UnityEngine.Random.Range(1,10)%2 == 1)
        {
            playAudio.IntervalsInOctave.TryGetValue(resultInterval,out intervalText);

            button1.gameObject.GetComponentInChildren<Text>().text = intervalText;

            button1.onClick.AddListener(wrongAnswer);

            //define random interval for second guess from selected intervals
            playAudio.IntervalsInOctave.TryGetValue(resultInterval, out wrongIntervalText);
            while (wrongIntervalText == intervalText)
            {
                if (customIntervalSettingsScriptable && !isDevMode)
                {
                    setSpecificIntervals(customIntervalSettingsScriptable.isInterval);
                }
                else
                {
                    setSpecificIntervals(isInterval);
                }
                playAudio.IntervalsInOctave.TryGetValue(resultInterval, out wrongIntervalText);
            }

            button2.gameObject.GetComponentInChildren<Text>().text = wrongIntervalText;

            button2.onClick.AddListener(goodAnswer);
        }
        else
        {
            playAudio.IntervalsInOctave.TryGetValue(resultInterval, out intervalText);

            button2.gameObject.GetComponentInChildren<Text>().text = intervalText;

            button2.onClick.AddListener(wrongAnswer);

            //define random interval for second guess
            playAudio.IntervalsInOctave.TryGetValue(resultInterval, out wrongIntervalText);
            while (wrongIntervalText == intervalText)
            {
                if (customIntervalSettingsScriptable && !isDevMode)
                {
                    setSpecificIntervals(customIntervalSettingsScriptable.isInterval);
                }
                else
                {
                    setSpecificIntervals(isInterval);
                }
                playAudio.IntervalsInOctave.TryGetValue(resultInterval, out wrongIntervalText);
            }

            button1.gameObject.GetComponentInChildren<Text>().text = wrongIntervalText;

            button1.onClick.AddListener(goodAnswer);
        }

        playAudio.playOctaveInterval(resultInterval, isUp, true, isStableNote);
    }


    void goodAnswer()
    {
        result += 1;
        playAudio.setBoolIsStableNote(false);
        changeButtons();

    }
    void wrongAnswer()
    {
        if(HP > 0)
        {
            HP--;
            setHPText();
            changeButtons();
        }
        else
        {
            gameOver.SetActive(true);
            game.SetActive(false);
        }
    }

    void setPlayIntervalButton()
    {
        playIntervalButton.onClick.AddListener(playInterval);
    }
    void playInterval()
    {
        playAudio.playOctaveInterval(resultInterval,isUp,true,isStableNote) ;
    }
    void setResultText()
    {
        //resultText.text = result.ToString();

        PersistentVariablesSource source = LocalizationSettings.StringDatabase.SmartFormatter.GetSourceExtension<PersistentVariablesSource>();
        IntVariable myResult = source["UIGlobal"]["result"] as IntVariable;
        myResult.Value = result;
    }
    private void setHPText()
    {
        if (HPText)
        {
            HPText.text = HP.ToString();
        }
        else
        {
            Debug.LogWarning("No HP text set in " + this);
        }
    }
}
