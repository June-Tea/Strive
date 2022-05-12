/*
This script manages the spiky ball obstacles that are spawned by another script
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spike : MonoBehaviour
{   
    //basic variables
    public int damage = 50;
    [SerializeField] private AudioSource hitSound;

    //Manages collisions with or without the player
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //ignores triggers
        if(!hitInfo.isTrigger)
        {
            //upon colliding with the player, deal damage
            Player player = hitInfo.GetComponent<Player>();
            if (player != null)
            {
                hitSound.Play();
                player.TakeDamage(damage);
            }
            //destroy this obstacle after it collides with anything
            Destroy(gameObject);
        }
    }
}
