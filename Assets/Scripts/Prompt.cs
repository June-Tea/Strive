/*
This script manages the pop-up menu UI that shows up if the player 
is able to put their name on the scoreboard
this script uses button_ui assets from the codemonkey package
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;

public class Prompt : MonoBehaviour
{
    //text and buttong components
    private TMP_Text title;
    private TMP_InputField input;
    private Button_UI submit;


    private void Awake () {
        //pass the approppriate references to the the components
        title = transform.Find("Title").GetComponent<TMP_Text>();
        input = transform.Find("input").GetComponent<TMP_InputField>();
        submit = transform.Find("Button").GetComponent<Button_UI>();

        //makes the window initially invisible
        Hide();
    }
    // Update is called once per frame
    private void Update(){
        //allows the player to activate the button's function upon pressing enter/return
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            submit.ClickFunc();
        }
    }
    //makes the object visible, changes the display texts and gives the button a function
    public void Show(string t, string i, Action<string> onSubmit) {
        gameObject.SetActive(true);
        title.text = t;
        input.text = i;
        //Returns a string to the caller upon pressing the button and also gets rid of the window
        submit.ClickFunc = () => {
            Hide();
            onSubmit(input.text);
        };

    }
    //hides the object and makes it uninteractable
    public void Hide() {
        gameObject.SetActive(false);
    }
}
