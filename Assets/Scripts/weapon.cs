/*
This script allows the player to fire projectiles from a certain point
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    //the referenced spawn point
    public Transform firepoint;
    //the referenced fireball prefab
    public GameObject firePrefab;
    //a delay to limit how much the player can fire
    public float fireDelay = .1f;
    [SerializeField] private AudioSource fireSound;

    // Update is called once per frame
    void Update()
    {
        //after a short delay, calls the shoot function and plays the fire sound effect
        fireDelay -= Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && fireDelay <= 0)
        {
            fireSound.Play();
            shoot();
            fireDelay = .1f;
        }
    }

    //creates a new projectile in the direction and position of the referenced firepoint
    void shoot ()
    {
        Instantiate(firePrefab, firepoint.position, firepoint.rotation);
    }
}
