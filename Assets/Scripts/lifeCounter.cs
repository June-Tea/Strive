/*
This script manages the UI for the player's lives
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class lifeCounter : MonoBehaviour
{
    //reference to our text component
    public TMP_Text counter;
    //changes the life counter to the input
    public void setCounter(string lives){
        counter.text = lives;
    }
}
