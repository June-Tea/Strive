/*
This script is referenceed by the explosion animation and destroys the object after animation
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    public void explode()
    {
        Destroy(gameObject);
    }
}
