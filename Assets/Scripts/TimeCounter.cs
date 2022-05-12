/*
This script manages the timer UI in the top right corner of the game
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    //reference to text component
    public TMP_Text counter;
    
    //formats the float time to better resemble common perceptions of what time looks like
    public void formatTime(float time)
    {
        int mins = (int) time/60000;
        int secs = (int) time/ 1000-60 * mins;
        setCounter(string.Format("{0:00}:{1:00}",mins,secs));
    }
    //Changes the UI text to match the current timer
    public void setCounter(string time){
        counter.text = time;
    }

}
