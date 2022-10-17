using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.XR.CoreUtils;
using System;
using System.Linq;

public class SpawnWords : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject WordPrefab;


    private TMP_Text wordOnObject;



    private float _maxAngle = 360.0f;
    private float _noOfWordsInScene = 20.0f;
    int _counter = 0;
    float _distance = 1000.0f;
    float _angle = 0.0f;
    [SerializeField]
    private float _timeToShowWord = 10.0f;
    private int _animateTime = 0;
    private int _animateMaxTime = 200;
    private List<GameObject> _wordsInScene = new List<GameObject>();
    private bool isAnimating = true;
    void Start()
    {
       _angle = _maxAngle/_noOfWordsInScene;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }


    public void SpawnPrefab(string name)
    {   Vector2 pos = SpawnPosition(_distance);
        Vector3 spawnPosition = new Vector3(pos.x, UnityEngine.Random.Range(-300.0f,500.0f), pos.y);
        
        var obj = Instantiate(WordPrefab, spawnPosition, Quaternion.identity);
        obj.name = name;
        Quaternion rotation = Quaternion.LookRotation(spawnPosition, obj.transform.up);
        obj.transform.rotation = rotation;
        wordOnObject = obj.GetComponentInChildren<TMP_Text>();
        wordOnObject.text = name;
        obj.tag = "Words";
        obj.transform.GetChild(1).name = name;
        _wordsInScene.Add(obj); 
        obj.SetActive(false);
    }

    private Vector2 SpawnPosition(float _distance)
    {
        _angle = _maxAngle / _noOfWordsInScene;
        var x = _distance * Mathf.Cos(_angle*_counter/(180.0f/MathF.PI));
        var y =  _distance* Mathf.Sin(_angle*_counter/ (180.0f / MathF.PI)); 
        _counter = _counter +1;
      

        Vector2 pos = new Vector2(x, y);
        return pos;
    
    }


    public IEnumerator Animate()
    {
       
        foreach (var word in _wordsInScene)
        {
            //Debug.Log(word.gameObject.name);
            word.SetActive(true);  
            yield return new WaitForSeconds(_timeToShowWord);
            word.SetActive(false);

        }
        
    }

}
