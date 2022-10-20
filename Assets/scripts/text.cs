using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using replicatedll;
using System;
using Replicate.Net.Models;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;
using UnityEngine.Networking;
using TMPro;
public class text : MonoBehaviour
{
    ReplicateDLL replicateDLL = new ReplicateDLL();

    string Token = "4975d3a2ecdedd9dfbac2b875621048781f38216";


   
    public GameObject imgObject;

    public TMP_InputField inputText;

    private Renderer rendere;
    Newtonsoft.Json.Linq.JArray array;
    private void Start()
    {

    }



 



    public async void GetImage()
    {
        string prompt = inputText.text; 
        if(prompt!=null)
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

        Texture myTexture = DownloadHandlerTexture.GetContent(www);

        if(myTexture != null)
        {
            rendere = imgObject.GetComponent<Renderer>();
            rendere.material.SetTexture("_MainTex", myTexture);
            rendere.material.SetTexture("_EmissionMap",myTexture );

        }
    }


}
