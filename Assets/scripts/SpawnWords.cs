using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.XR.CoreUtils;

public class SpawnWords : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject WordPrefab;


    private TMP_Text wordOnObject;
    

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }


    public void SpawnPrefab(string name)
    {
        Vector3 spawnPosition = new Vector3(Random.Range(1.0f, 5.0f), Random.Range(1.0f, 5.0f), Random.Range(5.0f, 10.0f));
        var obj = Instantiate(WordPrefab, spawnPosition, Quaternion.identity);
        obj.name = name;
        wordOnObject = obj.GetComponentInChildren<TMP_Text>();
        wordOnObject.text = name;

        obj.transform.GetChild(1).name = name;
    }
}
