/*
This script manages the healthbard UI and moves the slider representing the player's health
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    //Our UI components
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    
    //allows for the maximum healthvalue to be changed and fill the bar with a gradient
    //color that moves from green to red as it gets lower
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }
    //Allows for the current health value to be changed and moves the slider accordingly
    //giving the illusion of draining or increasing health
    public void SetHealth(int health)
    {
        slider.value = health;
        //changes the color of the bar according to the gradient
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
