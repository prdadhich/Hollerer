using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update

    public Database database;
    public float fadeDuration = 1.0f;
    public Color fadeColor;
    public Renderer rend;

    private void Start()
    {
        FadeIn();
    }
    public void SceneToLoad(string sceneName)
    {
        FadeOut(sceneName);
        //StartCoroutine(LoadLevel(sceneName));
        
    }

    IEnumerator LoadLevel(string sceneName)
    {

        yield return null;
        //yield return new WaitForSeconds(fadeDuration); 

        if (sceneName != "EntryScene")
        {
            database.ReplicatedJsonData.NumberofScenesLoaded = database.ReplicatedJsonData.NumberofScenesLoaded + 1;

            Debug.Log("NumberofScenesLoaded"+ database.ReplicatedJsonData.NumberofScenesLoaded);
            StartCoroutine(database.WriteJsonFile());
        }
        if (database.ReplicatedJsonData.NumberofScenesLoaded < 4)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            SceneManager.LoadScene("EndScene");
        }

    }

    public void FadeIn()
    {
        Fade(1, 0);
    }
    public void FadeOut(string sceneName)
    {
        Fade(0, 1);
        StartCoroutine(LoadLevel(sceneName));

    }
    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
    }


    public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        float timer = 0;

        while (timer < fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer/fadeDuration);


            rend.material.SetColor("_Color",newColor);
            timer += Time.deltaTime;
            yield return null;
        }

        Color newColor2 = fadeColor;
        newColor2.a = alphaOut;


        rend.material.SetColor("_Color", newColor2);

    }


}
