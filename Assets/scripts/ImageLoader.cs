using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEditor.Scripting.Python;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;

public class ImageLoader : MonoBehaviour
{
    private string _url;
    // This will be the most recent value from the replicate API link, we will store this in Firebase and
    // call the URL when this is updated, with python script..
    [SerializeField]
    private GameObject _videoPlayer;
    private VideoPlayer _waitingVideo;
  
   // public string url; 
    private Renderer _thisRenderer;
    public Database database;
    // automatically called when game started
    void Start()
    {
        PythonRunner.RunFile($"{Application.dataPath}/replicate_.py");
        StartCoroutine(DelayStart());
        _waitingVideo = _videoPlayer.GetComponent<VideoPlayer>();   
    }

    private IEnumerator DelayStart()
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

    private void Update()
    {
        //url = RecentUrl;
        //url = dataBase.url;
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

            _waitingVideo.Play();
        }
        else
        {
            _waitingVideo.Stop();
            _videoPlayer.SetActive(false);
        }

    }
}