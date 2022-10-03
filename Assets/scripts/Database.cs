using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading.Tasks;
using UnityEditor.VersionControl;
using System;
using UnityEditor.Scripting.Python;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;

public class Database : MonoBehaviour

{
  
    
    private string jsonData;
   
    [HideInInspector]
    public string url;
    [HideInInspector]
    public string JsonPath = "D:/Replicate.json";

    public SpawnWords spawnWords;
    public FocusCamera focusCamera;
    
 
    public static int GameStartCounter = 0;
   // public ImageLoader imgLoader;
    public struct ReplicateJsonData
    {
        public string Url;
        public string SelectedWords;
        public int NumberofScenesLoaded;
        public Dictionary<string, string> Words;
        public Dictionary<string, List<string>> Scenes;
    };
    [HideInInspector]
    public ReplicateJsonData ReplicatedJsonData;
    // Start is called before the first frame update
    void Start()
    {
      
        ReplicatedJsonData = JsonConvert.DeserializeObject<ReplicateJsonData>(File.ReadAllText(JsonPath));

        foreach(var SceneWord in ReplicatedJsonData.Scenes[SceneManager.GetActiveScene().name])
        {
            spawnWords.SpawnPrefab(SceneWord);
            focusCamera.WordsInScene.Add(SceneWord);
        }

        if(GameStartCounter ==0)
        {
            ReplicatedJsonData.SelectedWords = "";
            ReplicatedJsonData.NumberofScenesLoaded = 0;
            GameStartCounter++;
            StartCoroutine(WriteJsonFile());
        }
        
        if(SceneManager.GetActiveScene().name == "EndScene")
        {
            StartCoroutine(WriteJsonFile());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public IEnumerator WriteJsonFile()
    {



        //StreamReader reader = new StreamReader(path);
        //jsonData = reader.ReadToEnd();
        //reader.Close();

        // ReplicatedJsonData = JsonConvert.DeserializeObject<ReplicateJsonData>(File.ReadAllText(path));
        //ReplicatedJsonData.SelectedWords = TextArea.WordsSelected.text;

        //Debug.Log(ReplicatedJsonData.SelectedWords[0]);
        
       
        //Debug.Log(ReplicatedJsonData.SelectedWords[1]);
       // Debug.Log(JsonConvert.SerializeObject(ReplicatedJsonData).GetType());
        File.WriteAllText( JsonPath,JsonConvert.SerializeObject(ReplicatedJsonData));

        yield return new WaitForSeconds(0);

        
        if (SceneManager.GetActiveScene().name=="EndScene")
        {
            PythonRunner.RunFile($"{Application.dataPath}/replicate_.py");
            yield return new WaitForSeconds(2);

            ReadJsonFile();
        }
       
    }

    public void ReadJsonFile()
    {
        ReplicatedJsonData = JsonConvert.DeserializeObject<ReplicateJsonData>(File.ReadAllText(JsonPath));
        url = ReplicatedJsonData.Url;

        //imgLoader.LoadImage(url);
       /* foreach (var text in TextArea.WordsToSend)
        {
            ReplicatedJsonData.SelectedWords.Append(text);

        }*/

    }

}
