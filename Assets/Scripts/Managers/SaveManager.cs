using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;


    public SaveData activeSave;

    public bool hasLoaded;
    private void Awake()
    {
        instance = this;

        Load();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (SaveManager.instance.hasLoaded)
        {
            //ustaw co trzeba SaveManager.instance.activeSave.
        }
        else
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        string dataPath = Application.persistentDataPath;

        var serializer = new XmlSerializer(typeof(SaveData));

        var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".xd",FileMode.Create);

        serializer.Serialize(stream, activeSave);
        stream.Close();

        Debug.Log("Save Done");
    }
    public void Load()
    {
        string dataPath = Application.persistentDataPath;

        if(System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".xd"))
        {
            var serializer = new XmlSerializer(typeof(SaveData));

            var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".xd", FileMode.Open);

            activeSave = serializer.Deserialize(stream) as SaveData;
            stream.Close();

            Debug.Log("Load Done");
            hasLoaded = true;
        }
    }
    public void DeleteSaveData()
    {
        string dataPath = Application.persistentDataPath;
        if (System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".xd"))
        {
            File.Delete(dataPath + "/" + activeSave.saveName + ".xd");
        }

    }
}

[System.Serializable]
public class SaveData
{
    public string saveName;

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