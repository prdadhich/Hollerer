using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SkipVideo : MonoBehaviour
{
    public GameObject skipButton;
   
    
    // Start is called before the first frame update
    void Start()
    {
        skipButton.SetActive(false);
        StartCoroutine(ShowSkip());
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    IEnumerator ShowSkip()
    {
        yield return new WaitForSeconds(7);
        skipButton.SetActive(true);    
    }
}
