using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageLoader : MonoBehaviour
{
    public string RecentUrl;
    // This will be the most recent value from the replicate API link, we will store this in Firebase and
    // call the URL when this is updated, with python script..



   // public string url; 
    public Renderer thisRenderer;
    public Database dataBase;
    // automatically called when game started
    void Start()
    {
       // url = dataBase.url;
//
       // StartCoroutine(LoadFromLikeCoroutine()); // execute the section independently

        // the following will be called even before the load finished
        thisRenderer.material.color = Color.red;
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
        WWW wwwLoader = new WWW(url);   // create WWW object pointing to the url
        yield return wwwLoader;         // start loading whatever in that url ( delay happens here )

        Debug.Log("Loaded");
        thisRenderer.material.color = Color.white;              // set white
        thisRenderer.material.mainTexture = wwwLoader.texture;  // set loaded image
    }

    public void LoadImage( string url)
    {
        StartCoroutine(LoadFromLikeCoroutine(url));

    }
}