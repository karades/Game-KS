using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{

    int randomNum;
    AudioClip audio1;
    AudioClip audio2;
    public Dictionary<int, string> IntervalsInOctave = new Dictionary<int, string>()
    {
        {1,"1"},
        {2,"2>"},
        {3,"2"},
        {4,"3>"},
        {5,"3"},
        {6,"4"},
        {7,"4<"},
        {8,"5"},
        {9,"6>"},
        {10,"6"},
        {11,"7"},
        {12,"7<"},
        {13,"8"},
    };

    int[] major0 = new int[2] {5,4};
    int[] major1 = new int[2] { 4, 6 };
    int[] major2 = new int[2] { 6, 5 };

    int[] minor0 = new int[2] { 4, 5 };
    int[] minor1 = new int[2] { 5, 6 };
    int[] minor2 = new int[2] { 6, 4 };
    [SerializeField]
    private GameObject NotePlayer;

    private Object[] audioClips;

    [SerializeField]
    int inversion = 1;

    [SerializeField]
    int scale = 0;
    // Start is called before the first frame update
    void Start()
    {
        audioClips = Resources.LoadAll("samples", typeof(AudioClip));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            playOctaveInterval(8, true, true) ;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {

            playTriad(inversion, scale, true);
            

        }
        
    }


    void playRandomNote()
    {
        GameObject newNote = Instantiate(NotePlayer);
        AudioClip audio = (AudioClip)audioClips[Random.Range(0, audioClips.Length)];
        newNote.GetComponent<AudioSource>().clip = audio;
        newNote.GetComponent<AudioSource>().Play();
        Destroy(newNote, 2);

    }

    void playNote (int note)
    {
        GameObject newNote = Instantiate(NotePlayer);
        AudioClip audio = (AudioClip)audioClips[note];
        newNote.GetComponent<AudioSource>().clip = audio;
        newNote.GetComponent<AudioSource>().Play();
        Destroy(newNote, 4);
    }

    public void playOctaveInterval(int interval,bool isUp = false, bool isMelodic = false)
    {
        interval -= 1;
        GameObject newNote = Instantiate(NotePlayer);
        GameObject newNote2 = Instantiate(NotePlayer);
        randomNum = Random.Range(interval, audioClips.Length - interval);
        audio1 = (AudioClip)audioClips[randomNum];
        if (isUp)
        {
             audio2 = (AudioClip)audioClips[randomNum+interval];
        }
        else
        {
             audio2 = (AudioClip)audioClips[randomNum-interval];
        }

        newNote.GetComponent<AudioSource>().name = audio1.name;
        newNote2.GetComponent<AudioSource>().name = audio2.name;
        if (!isMelodic)
        {
            newNote.GetComponent<AudioSource>().PlayOneShot(audio1);
            newNote2.GetComponent<AudioSource>().PlayOneShot(audio2);
            Destroy(newNote, 1);
            Destroy(newNote2, 1);
        }
        else
        {
            StartCoroutine(playMelodicInterval(newNote, newNote2));
        }

    }

    IEnumerator playMelodicInterval(GameObject newNote, GameObject newNote2)
    {   
        newNote.GetComponent<AudioSource>().PlayOneShot(audio1);
        yield return new WaitForSeconds(0.5f);
        newNote2.GetComponent<AudioSource>().PlayOneShot(audio2);
        Destroy(newNote, 1);
        Destroy(newNote2, 1);
        yield return null;
    }
    IEnumerator playMelodicTriad(GameObject newNote, AudioClip audio1, AudioClip audio2, AudioClip audio3)
    {
        newNote.GetComponent<AudioSource>().PlayOneShot(audio1);
        yield return new WaitForSeconds(0.5f);
        newNote.GetComponent<AudioSource>().PlayOneShot(audio2);
        yield return new WaitForSeconds(0.5f);
        newNote.GetComponent<AudioSource>().PlayOneShot(audio3);
        yield return new WaitForSeconds(0.5f);
        Destroy(newNote, 1);

        yield return null;
    }
    IEnumerator playHarmonicTriad(GameObject newNote, AudioClip audio1, AudioClip audio2, AudioClip audio3)
    {
        newNote.GetComponent<AudioSource>().PlayOneShot(audio1);
        newNote.GetComponent<AudioSource>().PlayOneShot(audio2);
        newNote.GetComponent<AudioSource>().PlayOneShot(audio3);
        yield return new WaitForSeconds(0.5f);
        Destroy(newNote, 1);

        yield return null;
    }
    void playTriad(int inversion, int scale, bool isMelodic = false)
    {

        GameObject newNote = Instantiate(NotePlayer);
        randomNum = Random.Range(1, audioClips.Length - 13);

        switch (scale)
        {
            case 0:
            {

                if(inversion == 1)
                    {
                        
                        if (isMelodic) 
                        { 
                            StartCoroutine(playMelodicTriad(newNote, (AudioClip)audioClips[randomNum], (AudioClip)audioClips[randomNum + major0[0] - 1], (AudioClip)audioClips[randomNum + major0[1] + major0[0]-2])); 
                        }
                        else
                        {
                            StartCoroutine(playHarmonicTriad(newNote, (AudioClip)audioClips[randomNum], (AudioClip)audioClips[randomNum + major0[0] - 1], (AudioClip)audioClips[randomNum + major0[1] + major0[0]-2]));
                        }
                    }

                else if (inversion == 2)
                {
                        if (isMelodic)
                        {
                            StartCoroutine(playMelodicTriad(newNote, (AudioClip)audioClips[randomNum], (AudioClip)audioClips[randomNum + major1[0]-1 ], (AudioClip)audioClips[randomNum + major1[1] + major1[0] -2]));
                        }
                        else
                        {
                            StartCoroutine(playHarmonicTriad(newNote, (AudioClip)audioClips[randomNum], (AudioClip)audioClips[randomNum + major1[0] - 1], (AudioClip)audioClips[randomNum + major1[1] + major1[0] -2]));
                        }
                    }

                else if (inversion == 3)
                {
                        if (isMelodic)
                        {
                            StartCoroutine(playMelodicTriad(newNote, (AudioClip)audioClips[randomNum], (AudioClip)audioClips[randomNum + major2[0] - 1], (AudioClip)audioClips[randomNum + major2[1] + major2[0] - 2]));
                        }
                        else
                        {
                            StartCoroutine(playHarmonicTriad(newNote, (AudioClip)audioClips[randomNum], (AudioClip)audioClips[randomNum + major2[0] - 1], (AudioClip)audioClips[randomNum + major2[1] + major2[0] - 2]));
                        }
                }
                    break; 
            }
            case 1:
            {
                    if (inversion == 1)
                    {

                        if (isMelodic)
                        {
                            StartCoroutine(playMelodicTriad(newNote, (AudioClip)audioClips[randomNum], (AudioClip)audioClips[randomNum + minor0[0] - 1], (AudioClip)audioClips[randomNum + minor0[1] + minor0[0] - 2]));
                        }
                        else
                        {
                            StartCoroutine(playHarmonicTriad(newNote, (AudioClip)audioClips[randomNum], (AudioClip)audioClips[randomNum + minor0[0] - 1], (AudioClip)audioClips[randomNum + minor0[1] + minor0[0] - 2]));
                        }
                    }

                    else if (inversion == 2)
                    {
                        if (isMelodic)
                        {
                            StartCoroutine(playMelodicTriad(newNote, (AudioClip)audioClips[randomNum], (AudioClip)audioClips[randomNum + minor0[0] - 1], (AudioClip)audioClips[randomNum + minor0[1] + minor0[0] - 2]));
                        }
                        else
                        {
                            StartCoroutine(playHarmonicTriad(newNote, (AudioClip)audioClips[randomNum], (AudioClip)audioClips[randomNum + minor0[0] - 1], (AudioClip)audioClips[randomNum + minor0[1] + minor0[0] - 2]));
                        }
                    }

                    else if (inversion == 3)
                    {
                        if (isMelodic)
                        {
                            StartCoroutine(playMelodicTriad(newNote, (AudioClip)audioClips[randomNum], (AudioClip)audioClips[randomNum + minor0[0] - 1], (AudioClip)audioClips[randomNum + minor0[1] + minor0[0] - 2]));
                        }
                        else
                        {
                            StartCoroutine(playHarmonicTriad(newNote, (AudioClip)audioClips[randomNum], (AudioClip)audioClips[randomNum + minor0[0] - 1], (AudioClip)audioClips[randomNum + minor0[1] + minor0[0] - 2]));
                        }
                    }
                    break;
            }
        }


    }

}
