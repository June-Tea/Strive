/*
This script manages the projectiles fired by enemies and is very similar to the bullet script
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    //basic variables
    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 10;

    // Start is called before the first frame update
    void Start()
    {   //Makes the projectile zoom as soon as it spawns
        rb.velocity = transform.right * speed;
    }

    //Manages projectile collisions
    void OnTriggerEnter2D (Collider2D hitInfo)
    {
        //ignores triggers just like the bullet script
        if(hitInfo.tag != "Room" && !hitInfo.isTrigger)
        {
            //checks the projectile hits the PLAYER and deals aproppriate damage
            Player player = hitInfo.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
            //Destroy object to reduce clutter
            Destroy(gameObject);
        }
    }

}
