/*
This script manages the spawn points that spawn spiky balls at regular intervals
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class spiker_spawner : MonoBehaviour
{
    //spawn interval
    public float spawnDelay = 1f;
    //reference to our spiky ball prefab
    public GameObject spike;
    //Variables to determin how far the player is
    public float range = 100;
    public Transform player;

    [SerializeField] private AudioSource ballSound;


    // Update is called once per frame
    void Update()
    {
        //if the player is within range, spawn balls at an interval
        float distance = Vector2.Distance(transform.position, player.position);
        spawnDelay -= Time.deltaTime;
        
        if (spawnDelay <= 0 && distance <= range)
        {
            spawn();
            spawnDelay = 1f;
        }
    }

    //creates a new spiky ball and plays a sound
    void spawn()
    {
        Instantiate (spike, transform. position, transform. rotation);
        ballSound.Play();
    }
}
