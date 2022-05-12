/*
This simple script is referenced by the diamond on the last level
and lets the player know they've completed the game by taking
them to the appropriate scene
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    //Upon collision with player loads the endscreen scene
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Player player = collision.GetComponent<Player>();
        if (player != null)
        {   //stores a special distance value and the player's current time in the static scoreHolder class
            ScoreHolder.setDistance(7777777f);
            ScoreHolder.setTime(player.time);
            SceneManager.LoadScene("EndScreen");
        }

    }
}
