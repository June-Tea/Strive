/*
This script handles the basic elements of all enemies such as health, 
contact damage, death sounds and animations
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //basic variables
    public int health = 100;
    public int damage;
    public float damageDelay = .5f;
    private float hitTime = 0;
    //Death animation
    public GameObject deatheffect;
    
    //allows the enemy to take damage  
    public void TakeDamage (int damage)
    {
        health -= damage;
        //calls the die function when enemy's health drops below 0
        if (health <= 0)
        {
            Die();
        }
    }
    //Checks if enemy collides with the player
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject entity = collision.gameObject;
        if (entity.CompareTag("Player"))
        {
            //upon collision with player do damage
            Player player = entity.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
                hitTime = damageDelay; //damage buffer
            }
        }
    }
    //Continues to do damage so long as the player stays in contact
    void OnCollisionStay2D(Collision2D collision){
        GameObject entity = collision.gameObject;
        if (entity.CompareTag("Player") && hitTime <= 0)
        {
            Player player = entity.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
                hitTime = damageDelay; //damage buffer; makes sure player doesn't immediately die upon coming in contact with enemies
            }
            hitTime -= Time.deltaTime;
        }
    }
    //When enemy dies play the appropriate sounds and animations before destroying the object
    void Die()
    {
        Instantiate(deatheffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}