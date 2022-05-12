/*
This simple script allows the player to change from the game over scene to the scoreboard scene
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverScreen : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //Automatically moves to the next scene after 10 seconds
        Invoke("nextScene",10f);
        //Also moves if player presses anything on their keyboard, mouse, etc.
        if (Input.anyKey)
        {
            nextScene();
        }
    }
    //Loads the next scene, the score board
    public void nextScene(){
        SceneManager.LoadScene("ScoreBoard");
    }
}
