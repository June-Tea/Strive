/*
This script manages the green objects that send
the player flying to the next level
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncy : MonoBehaviour
{
    //upward force
    public float bounce = 20f;
    //components
    Rigidbody2D rb;
    [SerializeField] private AudioSource bounceSound;

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D other)
    {   //when the player collides with the object it sends them flying up and plays a sound
        if (other.gameObject.CompareTag("Player"))
        {
            bounceSound.Play();
            rb = other.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.up * bounce, ForceMode2D.Impulse);   
        }
    }
}
