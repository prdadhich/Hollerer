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
using System.Diagnostics.Contracts;
using UnityEngine.XR.OpenXR;
using Unity.VisualScripting;
using Microsoft.Scripting.Utils;
using TMPro;

public class SpawnSelectedWords : MonoBehaviour
{

    public struct ReplicateJsonData
    {
        public string Url;
        public string SelectedWords;
        public int NumberofScenesLoaded;
        public Dictionary<string, string> Words;
        public Dictionary<string, List<string>> Scenes;
    };

    public ReplicateJsonData ReplicatedJsonData;


    public GameObject selectedWord;

    private TMP_Text wordOnObject;
    int _counter = 0;
    float  _distance = 600.0f;
    float _angle = 0.0f;

    private float _maxAngle = 360.0f;
    private float _noOfWordsInScene = 20.0f;
    [HideInInspector]
    public string JsonPath = null;
    private  List<string> selectedWords = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        JsonPath = $"{Application.dataPath}/Replicate.json";
        ReplicatedJsonData = JsonConvert.DeserializeObject<ReplicateJsonData>(File.ReadAllText(JsonPath));

        selectedWords = ReplicatedJsonData.SelectedWords.Split(",").ToList();
        selectedWords.Remove("");
        Debug.Log("Count" + selectedWords.Count);


        SpawnObject();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SpawnObject()
    {
        
        foreach (var obj in selectedWords)
        {
            Vector2 pos = SpawnPosition(_distance);
            Vector3 spawnPosition = new Vector3(pos.x, -800.0f, pos.y);
            var selectedGameObj = Instantiate(selectedWord, spawnPosition,Quaternion.identity);
            selectedGameObj.gameObject.name = obj;
            Quaternion rotation = Quaternion.LookRotation(spawnPosition, selectedGameObj.transform.up);
            selectedGameObj.transform.rotation = rotation;
            wordOnObject = selectedGameObj.GetComponentInChildren<TMP_Text>();
            wordOnObject.text = obj;

        }
    }
    private Vector2 SpawnPosition(float _distance)
    {
        _angle = _maxAngle / _noOfWordsInScene;
        var x = _distance * Mathf.Cos(_angle * _counter / (180.0f / MathF.PI));
        var y = _distance * Mathf.Sin(_angle * _counter / (180.0f / MathF.PI));
        _counter = _counter + 1;


        Vector2 pos = new Vector2(x, y);
        return pos;

    }

}
