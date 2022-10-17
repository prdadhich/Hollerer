using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update

    public Database database;
    
    public void SceneToLoad(string sceneName)
    {
        
        
        if(sceneName != "EntryScene")
        {
            database.ReplicatedJsonData.NumberofScenesLoaded++;
            StartCoroutine(database.WriteJsonFile());
        }
        if (database.ReplicatedJsonData.NumberofScenesLoaded <4)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            SceneManager.LoadScene("EndScene");
        }
    }
}
