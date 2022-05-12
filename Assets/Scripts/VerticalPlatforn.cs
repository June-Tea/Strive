/*
This script manages the wooden, passable platforms and 
allows the player to pass through after holding the down key
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatforn : MonoBehaviour
{
    //components and wait variable
    private GameObject player;
    public float waitTime;
    private BoxCollider2D platform;

    // Update is called once per frame
    void Update()
    {
        //if the player lets go of the down key reset the timer
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            waitTime = 0.5f;
        }
        if(Input.GetKey(KeyCode.DownArrow)) 
        {   //if the player holds down the down key for long enough, let them pass and reset the timer
            if(waitTime <= 0 && player != null) 
            {
                StartCoroutine(DisableCollision(player));
                waitTime = 0.5f;
            }
            else {
                waitTime -= Time.deltaTime;
            }
        
        } 
    }
    //creates a reference to the player object upon collision
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
        }
    }
    //breaks the reference once the player leaves
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = null;
        }
    }

    //disables collisions with the player for a quarter of a second so the player can pass through
    private IEnumerator DisableCollision(GameObject player)
    {
        platform = GetComponent<BoxCollider2D>();
        BoxCollider2D playerBox = player.GetComponent<BoxCollider2D>();
        CircleCollider2D playerCircle = player.GetComponent<CircleCollider2D>();

        Physics2D.IgnoreCollision(playerCircle, platform);
        Physics2D.IgnoreCollision(playerBox, platform); 
        yield return new WaitForSeconds(0.25f);

        Physics2D.IgnoreCollision(playerCircle, platform, false);
        Physics2D.IgnoreCollision(playerBox, platform, false); 
    }
}
