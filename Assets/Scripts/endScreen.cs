/*
This simple screen allows the player to navigate to another scene
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endScreen : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        //loads the scoreboard if the player presses anything on the keyboard, mouse, etc.
        if (Input.anyKey)
        {
            SceneManager.LoadScene("ScoreBoard");
        }
    }
}
