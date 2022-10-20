using Replicate.Net.Models;
using replicatedll;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEditor.Scripting.Python;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;
using System.Linq;
using Unity.VisualScripting;
using System.Runtime;
using TMPro;

public class ImageLoader : MonoBehaviour
{
    ReplicateDLL replicateDLL = new ReplicateDLL();

    public string Token = "";


    
    Newtonsoft.Json.Linq.JArray array;

    private string _url;
   
    [SerializeField]
    private GameObject _videoPlayer;
    private VideoPlayer _waitingVideo;
  
   // public string url; 
    public Renderer[] _thisRenderer;
    public Database database;

    private List<string> _selectedWord = new List<string>();
    private string _finalPrompt;
    private static System.Random rnd = new System.Random();
    // automatically called when game started
    void Start()
    {
        database.ReadJsonFile();
        //PythonRunner.RunFile($"{Application.dataPath}/replicate_.py");
        ConvertPrompt();
        _waitingVideo = _videoPlayer.GetComponent<VideoPlayer>();
        //GetImage("lion king forest");
        _waitingVideo.Play();
    }

    private void ConvertPrompt()
    {
        _selectedWord= database.ReplicatedJsonData.SelectedWords.Split(",").ToList();
        _selectedWord.Remove("");
        Debug.Log(_selectedWord[0]);
        List<string> finalRandomSelectedWords = new List<string>();

        for(int i = 0; i<3;i++)
        {
            var index = rnd.Next(_selectedWord.Count);
            Debug.Log("index"+index);
            finalRandomSelectedWords.Add(_selectedWord[index]);
            _selectedWord.RemoveAt(index);  

        }
        
        foreach(var word in finalRandomSelectedWords)
        {
            _finalPrompt = _finalPrompt + database.ReplicatedJsonData.Words[word] + ",";

        }
        if(_finalPrompt!=null)
        GetImage(_finalPrompt);

    }

   /* private IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(1);
        database.ReadJsonFile();
        _url = database.url;
        StartCoroutine(LoadFromLikeCoroutine(_url));
        // StartCoroutine(LoadFromLikeCoroutine()); // execute the section independently
        _thisRenderer = this.GetComponent<Renderer>();
        // the following will be called even before the load finished


        _thisRenderer.material.EnableKeyword("_NORMALMAP");
        _thisRenderer.material.EnableKeyword("_EMISSIONMAP");
    }

 
    // this section will be run independently
    private IEnumerator LoadFromLikeCoroutine(string url)
    {
        Debug.Log("Loading ....");
        PlayAnimation(true);
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url); // create WWW object pointing to the url


        yield return www.SendWebRequest();
        Texture myTexture = DownloadHandlerTexture.GetContent(www);// start loading whatever in that url ( delay happens here )
        PlayAnimation(false);



        Debug.Log("Loaded");
        Debug.Log(_url);

        // set white
        _thisRenderer.material.SetTexture("_MainTex", myTexture);
        _thisRenderer.material.SetTexture("_EmissionMap", myTexture); // set loaded image

    }
  

    private void PlayAnimation(bool shouldPlay)

    {
        if (shouldPlay)
        {
            _videoPlayer.SetActive(true);

            
        }
        else
        {
          
            _videoPlayer.SetActive(false);
        }

    }


    */




    public async void GetImage(string prompt)
    {
        
        if (prompt != null)
        {

            Prediction response = await replicateDLL.PredictImage(Token, prompt);
            Debug.Log(response.Urls.Get);
            Debug.Log(response.Status);

            array = (Newtonsoft.Json.Linq.JArray)response.Output;
            Debug.Log(array[0]);
            Debug.Log(response.Output.ToString());
            Debug.Log(response.Output.GetType());

            StartCoroutine(GetTexture(array[0].ToString()));
        }

    }


    IEnumerator GetTexture(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();
        _waitingVideo.Stop();
        Texture myTexture = DownloadHandlerTexture.GetContent(www);

        if (myTexture != null)
        {
            foreach(var rend in _thisRenderer)
            {

                rend.material.SetTexture("_MainTex", myTexture);
                rend.material.SetTexture("_EmissionMap", myTexture);
            }
          

        }
    }

}