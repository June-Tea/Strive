/*
This simple script starts the actual gameplay
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    //this method is refferenced by the button in the start screen and loads the game scene when called
    public void StartGame()
    {
        SceneManager.LoadScene("Main_Gameplay");
    }
}    
