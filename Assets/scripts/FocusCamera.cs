using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class FocusCamera : MonoBehaviour
{

 


    public Camera CameraRay;
    public GameObject PointerPrefab;
    public float FocusTime = 1.0f;


    //material
    public Material HoverMaterial;
    public Material FocusedMaterial;
    public Material OriginalMaterial;

    public LoadScene loadScene;
    public Database database;

    private Collider _previousHitCollider;
    private Vector3 _pointerPosition;
    private int CallFunctionCounter = 0;
    
    float _startTime = 0;
    [HideInInspector]
    public List<string> WordsInScene = new List<string>();
    private  List<string> WordsSelectedInScene = new List<string>();


    public VideoPlayer videoPlayer;

    private List<string> _sceneNames = new List<string>
    {
        "Scene01","Scene02","Scene03","Scene04","Scene05","Scene06"
    };
    [HideInInspector]
    public static List<string> sceneAlreadyLoaded = new List<string>
    {
        
    };
    RaycastHit hit;
    Ray cameraRay;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.loopPointReached += MainSceneLoad;
    }


    private void FixedUpdate()
    {
        cameraRay = new Ray(CameraRay.transform.position, CameraRay.transform.forward);
        _pointerPosition = cameraRay.GetPoint(4);
        PointerPrefab.transform.position = _pointerPosition;
        CheckClick();

}
// Update is called once per frame
void Update()
    {
      
    }

    private void CheckClick()
    {


       
            if (Physics.Raycast(CameraRay.transform.position, cameraRay.direction, out hit))
            {

                if (hit.collider.gameObject.GetComponent<MeshRenderer>().material != FocusedMaterial)
                { hit.collider.gameObject.GetComponent<MeshRenderer>().material = HoverMaterial; }

                if (hit.collider == _previousHitCollider)
                {
               

                    if (_startTime + FocusTime < Time.time)
                    {
                        hit.collider.gameObject.GetComponent<MeshRenderer>().material = FocusedMaterial;
                        CallFunctionCounter += 1;
                        if (CallFunctionCounter == 1)
                        {
                   

                            //call the desired function here when things are kept focused

                            //for loading scene
                            if (_sceneNames.Contains(_previousHitCollider.name) && !sceneAlreadyLoaded.Contains(_previousHitCollider.name))
                            {
                                LoadRequiredScene(_previousHitCollider.name);
                                //loadScene.SceneToLoad(previousHitCollider.name);
                               sceneAlreadyLoaded.Add(_previousHitCollider.name);  
                            }


                            //for writing selected words 
                            if (WordsInScene.Contains(_previousHitCollider.name))
                            {
                                SaveSelectedWord(_previousHitCollider.name);
                                WordsSelectedInScene.Add(_previousHitCollider.name);

                            }
                            if (_previousHitCollider.name == "EntryScene")
                            {
                                loadScene.SceneToLoad("EntryScene");
                            }
                            if (_previousHitCollider.name == "Restart")
                            {
                                RestartGame();
                            }
                            if (_previousHitCollider.name == "Skip")
                            {
                                loadScene.SceneToLoad("EntryScene");
                            }


                    }
                    }

                }
                else
                {
                    _startTime = Time.time;
                    _previousHitCollider = hit.collider;
                   
                    CallFunctionCounter = 0;
                }
            }

            else
            {
                _startTime = 0.0f;
                //hit.collider.gameObject.GetComponent<MeshRenderer>().material = OriginalMaterial;

                _previousHitCollider = null;
            }
        
        
        


    }

    private void LoadRequiredScene(string sceneToLoad)
    {
        loadScene.SceneToLoad(sceneToLoad);
        if(sceneToLoad != "EntryScene")
        { 
            sceneAlreadyLoaded.Add(sceneToLoad); 
        }
        
    }

    private void SaveSelectedWord(string word)
    {
       
            database.ReplicatedJsonData.SelectedWords = word + "," + database.ReplicatedJsonData.SelectedWords;
            StartCoroutine(database.WriteJsonFile());
        
    }



    public void RestartGame()
    {
        Database.GameStartCounter = 0;
        SceneManager.LoadScene("IntroScene");
    }


    private void MainSceneLoad(VideoPlayer videoPlayer)
    {
        if(SceneManager.GetActiveScene().name != "EntryScene")
        LoadRequiredScene("EntryScene");

        if(database.ReplicatedJsonData.NumberofScenesLoaded ==3)
        {
            LoadRequiredScene("EndScene");
        }
    }
}
