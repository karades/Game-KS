using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    int result = 0;

    [SerializeField]
    int HP = 3;

    int resultInterval;

    string wrongIntervalText;
    string intervalText;

    //mo¿e nie tutaj, na test kwarty i kwinty, zostawione do deva
    bool[] isInterval = new bool[] { true,false,false,false,false,true, false, true, false, false,false,false,true };

    List<int> intervalsToGuess = new List<int>();

    [SerializeField]
    bool isStableNote = true;

    [SerializeField]
    bool isDevMode = true;
    void Start()
    {

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
        //co z isInterval? Z innej klasy?
        for (int i = 0; i < isInterval.Length; i++)
        {
            if (isInterval[i])
            {
                intervalsToGuess.Add(i+1);
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

        //setRandomInterval();
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

        playAudio.playOctaveInterval(resultInterval, false, true, isStableNote);
    }


    void goodAnswer()
    {
        result += 1;
        changeButtons();
        playAudio.setBoolIsStableNote(false);
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
            //end
        }
    }

    void setPlayIntervalButton()
    {
        playIntervalButton.onClick.AddListener(playInterval);
    }
    void playInterval()
    {
        playAudio.playOctaveInterval(resultInterval,true,true,isStableNote) ;
    }
    void setResultText()
    {
        resultText.text = result.ToString();
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
