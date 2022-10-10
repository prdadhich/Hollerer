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

    [SerializeField] 
    private InputActionAsset _playerControl;

    // Start is called before the first frame update
    void Start()
    {

       


    }

    // Update is called once per frame
    void Update()
    {
        //var _actionMap = _playerControl.FindActionMap("HMD");
      //  _userPresence = _actionMap.FindAction("hmdUserPresence");
       // Debug.Log(_userPresence.ReadValue<bool>());
    }


    private void OnEnable()
    {

        var _actionMap = _playerControl.FindActionMap("HMD");
        action = _actionMap.FindAction("hmdUserPresence");
        action.Enable();
        action.started += (ctx) => Debug.Log("UserPresence: true");
        action.canceled += Read;

    }
    private void Read(InputAction.CallbackContext context)
    {

        Database.GameStartCounter = 0;
        SceneManager.LoadScene("EntryScene");
        //StartCoroutine(RestartGame(context));
    }

    private IEnumerator RestartGame(InputAction.CallbackContext context)
    {
        yield return new WaitForSeconds(4);
        if(context.canceled)
        {
            Database.GameStartCounter = 0;
            SceneManager.LoadScene("EntryScene");
        }
      

    }
}
