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

    int result = 0;

    int resultInterval;

    string wrongIntervalText;
    string intervalText;

    //mo¿e nie tutaj, na test kwarty i kwinty
    bool[] isInterval = new bool[] { false,false,false,false,false,true, false, true, false, false,false,false,false };
    List<int> intervalsToGuess = new List<int>();

    void Start()
    {
        changeButtons();
        setResultText();
        setPlayIntervalButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setRandomInterval()
    {
        resultInterval = Random.Range(1,13);
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
        getIntervalsList();
        resultInterval = intervalsToGuess[Random.Range(0, intervalsToGuess.Count)];
    }
    void changeButtons()
    {
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();

        setResultText();

        //setRandomInterval();
        setSpecificIntervals(isInterval);

        if (Random.Range(1,10)%2 == 1)
        {
            playAudio.IntervalsInOctave.TryGetValue(resultInterval,out intervalText);

            button1.gameObject.GetComponentInChildren<Text>().text = intervalText;

            button1.onClick.AddListener(goodAnswer);

            //define random interval for second guess
            playAudio.IntervalsInOctave.TryGetValue(Random.Range(1, 13), out wrongIntervalText);
            while (wrongIntervalText == intervalText)
            {
                playAudio.IntervalsInOctave.TryGetValue(Random.Range(1, 13), out wrongIntervalText);
            }

            button2.gameObject.GetComponentInChildren<Text>().text = wrongIntervalText;

            button2.onClick.AddListener(changeButtons);
        }
        else
        {
            playAudio.IntervalsInOctave.TryGetValue(resultInterval, out intervalText);

            button2.gameObject.GetComponentInChildren<Text>().text = intervalText;

            button2.onClick.AddListener(goodAnswer);

            //define random interval for second guess
            playAudio.IntervalsInOctave.TryGetValue(Random.Range(1, 13), out wrongIntervalText);
            while (wrongIntervalText == intervalText)
            {
                playAudio.IntervalsInOctave.TryGetValue(Random.Range(1, 13), out wrongIntervalText);
            }

            button1.gameObject.GetComponentInChildren<Text>().text = wrongIntervalText;

            button1.onClick.AddListener(changeButtons);
        }
        playAudio.playOctaveInterval(resultInterval, false, true);
    }


    void goodAnswer()
    {
        result += 1;
        changeButtons();
    }

    void setPlayIntervalButton()
    {
        playIntervalButton.onClick.AddListener(playInterval);
    }
    void playInterval()
    {
        playAudio.playOctaveInterval(resultInterval) ;
    }
    void setResultText()
    {
        resultText.text = result.ToString();
    }
}
