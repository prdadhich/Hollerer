using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class Restart : MonoBehaviour
{

    

    private InputAction action;

    //[SerializeField] 
    //private InputActionAsset _playerControl;

    private UnityEngine.XR.InputDevice device;
    private bool _headsetRemoved = false;
    // Start is called before the first frame update
    void Start()
    {
    }
    private void OnEnable()
    {
        if(!device.isValid)
        {
            GetDevice();
        }
    }
    void GetDevice()
    {
       device =  InputDevices.GetDeviceAtXRNode(XRNode.Head);
    }
    // Update is called once per frame
    void Update()
    {
        //var _actionMap = _playerControl.FindActionMap("HMD");
        //  _userPresence = _actionMap.FindAction("hmdUserPresence");
        // Debug.Log(_userPresence.ReadValue<bool>());

        if (!device.isValid)
        {
            GetDevice();
        }
        device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.userPresence, out _headsetRemoved);
        //Debug.Log(_headsetRemoved);
        if (!_headsetRemoved)
        {

            StartCoroutine(RestartGame());
        }
    }


   /* private void OnEnable()
    {

        var _actionMap = _playerControl.FindActionMap("HMD");
        action = _actionMap.FindAction("hmdUserPresence");
        action.Enable();
        action.started += (ctx) => { Debug.Log("UserPresence: started"); _headsetRemoved = false; };

        action.performed += (ctx) => { Debug.Log("UserPresence: performed"); _headsetRemoved = false; };
        action.canceled += (ctx) => { Debug.Log("UserPresence: cancelled"); _headsetRemoved = true; };

    }
    private void OnDisable()
    {
        
        var _actionMap = _playerControl.FindActionMap("HMD");
        action = _actionMap.FindAction("hmdUserPresence");
        action.Disable();
        action.started -= (ctx) => Debug.Log("UserPresence: false");
        action.canceled -= (ctx) => Debug.Log("UserPresence: false");
        

    }
    private void Read(InputAction.CallbackContext context)
    {
        
        //Database.GameStartCounter = 0;
       // SceneManager.LoadScene("EntryScene");
        //StartCoroutine(RestartGame(context.ReadValue<bool>()));
    }*/

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(4);
        if(!_headsetRemoved)
        {
            Database.GameStartCounter = 0;
            FocusCamera.sceneAlreadyLoaded = null;
            SceneManager.LoadScene("IntroScene");
        }
        
        
     

    }
}
