/*
This script allows the camera to move between levels
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    //references a virtual camera from the cinemachine package
    public GameObject virtualCam;

    //Upon entering a new room turn on the camera so the view shifts over
    private void OnTriggerEnter2D(Collider2D other)
    {   //Ignores triggers
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(true);
        }
    }

    //turns off the camera upon leaving the room for a smoother view switch
    private void OnTriggerExit2D(Collider2D other)
    {   //ignores triggers
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(false);
        }
    }
}
