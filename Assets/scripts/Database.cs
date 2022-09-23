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

public class Database : MonoBehaviour

{
  
    
    private string jsonData;
    private LoadText TextArea;
    [HideInInspector]
    public string url;


    public ImageLoader imgLoader;
    public struct ReplicateJsonData
    {
        public string Url;
        public string SelectedWords;
        public Dictionary<string, string> Words;
    };

    ReplicateJsonData ReplicatedJsonData;
    // Start is called before the first frame update
    void Start()
    {
      
       TextArea = GetComponent<LoadText>();


        ReplicatedJsonData = JsonConvert.DeserializeObject<ReplicateJsonData>(File.ReadAllText("D:/Replicate.json"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void CallData()
    {

        //ReadJsonFile("D:/Replicate.json");
       StartCoroutine( WriteJsonFile("D:/Replicate.json"));

        

      
    }

    public IEnumerator WriteJsonFile(string path)
    {



        //StreamReader reader = new StreamReader(path);
        //jsonData = reader.ReadToEnd();
        //reader.Close();

        ReplicatedJsonData = JsonConvert.DeserializeObject<ReplicateJsonData>(File.ReadAllText(path));
        ReplicatedJsonData.SelectedWords = TextArea.WordsSelected.text;

        //Debug.Log(ReplicatedJsonData.SelectedWords[0]);
        
       
        //Debug.Log(ReplicatedJsonData.SelectedWords[1]);
       // Debug.Log(JsonConvert.SerializeObject(ReplicatedJsonData).GetType());
        File.WriteAllText( path,JsonConvert.SerializeObject(ReplicatedJsonData));

        yield return new WaitForSeconds(1);



        PythonRunner.RunFile($"{Application.dataPath}/replicate_.py");

        ReadJsonFile(path);
    }

    public void ReadJsonFile(string path)
    {
        ReplicatedJsonData = JsonConvert.DeserializeObject<ReplicateJsonData>(File.ReadAllText(path));
        url = ReplicatedJsonData.Url;

        imgLoader.LoadImage(url);
       /* foreach (var text in TextArea.WordsToSend)
        {
            ReplicatedJsonData.SelectedWords.Append(text);

        }*/

    }

}
