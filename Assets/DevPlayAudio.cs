using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevPlayAudio : MonoBehaviour
{
    [SerializeField]
    Button Triad1;
    [SerializeField]
    Button Triad2;
    [SerializeField]
    Button Triad3;

    [SerializeField]
    Button Dominant7;
    [SerializeField]
    Button Dominant56;
    [SerializeField]
    Button Dominant34;
    [SerializeField]
    Button Dominant2;
    [SerializeField]
    PlayAudio playAudio;
    [SerializeField]
    Toggle isMelodic;
        
    // Update is called once per frame
    void Start()
    {
        Triad1.onClick.AddListener(delegate { playAudio.playTriad(1, 0, isMelodic.isOn); });
        Triad2.onClick.AddListener(delegate { playAudio.playTriad(2, 0, isMelodic.isOn); });
        Triad3.onClick.AddListener(delegate { playAudio.playTriad(3, 0, isMelodic.isOn); });

        Dominant7.onClick.AddListener(delegate { playAudio.playDominant(1, isMelodic.isOn); });
        Dominant56.onClick.AddListener(delegate { playAudio.playDominant(2, isMelodic.isOn); });
        Dominant34.onClick.AddListener(delegate { playAudio.playDominant(3, isMelodic.isOn); });
        Dominant2.onClick.AddListener(delegate { playAudio.playDominant(4, isMelodic.isOn); });
    }
    void Update()
    {

    }
}
