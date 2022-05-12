/*
This script allows for certain areas to be designated out of bounds, as in lethal
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oob : MonoBehaviour
{
    //if the object touches the player, automatically kill them
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(999999);
        }

    }
}
