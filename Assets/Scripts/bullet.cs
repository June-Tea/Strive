/*
This scrip manages the projectile fired by the player
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    //basic components
    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 10;

    // Start is called before the first frame update
    void Start()
    {   //as soon as it's spawned the projectile speeds ahead
        rb.velocity = transform.right * speed;
    }

    //Checks what the project hits and responds appropriately
    void OnTriggerEnter2D (Collider2D hitInfo)
    {
        //makes projectile ignore triggers
        if(hitInfo.tag != "Room" && !hitInfo.isTrigger)
        {
            //upon hitting an enemy do damage
            Enemy enemy = hitInfo.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            //destroys the projectile on collision so as to preserve memory and processing power
            Destroy(gameObject);
        }
    }
}
