using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

using System.Linq;

public class LoadText : MonoBehaviour
{
    // Start is called before the first frame update

    public List<Button> WordsButton;
    public Button ResetBtn;
    [SerializeField] 
    public TMP_Text WordsSelected;

    
    int index = 0;
    void Start()
    {
        WordsSelected.text = "";
        foreach (var button in WordsButton)
        {
            button.onClick.AddListener(() => GetButtonName(""));
        }
        ResetBtn.onClick.AddListener(() => ResetWordsSelected());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetWordsSelected()
    {
        WordsSelected.text = "";
    }

    public void GetButtonName(string TextForFireBase)
    {
        
        if(index<5)
        {
            string currentName = EventSystem.current.currentSelectedGameObject.name;
            //Debug.Log(EventSystem.current.currentSelectedGameObject.name);
            WordsSelected.text = WordsSelected.text + currentName + ",";
           
        }
        index++;
    }
}
