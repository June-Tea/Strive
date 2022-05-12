/*
This script uses a reference to another script and a reference to the player to find 
and follow the player. The seeker string is part of the free A* that allows for easier pathfinding
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BirdAI : MonoBehaviour
{
    //A reference to the player's position
    public Transform target;
    //Movement variables
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    //Pathfinding variables
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    public float range = 50;
    //Components
    Seeker seeker;
    Rigidbody2D rb;
    //Orientation tracker
    bool facingRight = false;

    // Start is called before the first frame update
    void Start()
    {
        //get all our components
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        //calls update path every five seconds
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }
    //Checks if the seeker finished making a path then makes a new one
    void UpdatePath()
    {
        if(seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }
    //when path is complete makes a new one
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }   
        //Checks whether the entity reached the end of it's path
        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        } else {
            reachedEndOfPath = false;
        }
        //Variables for movement along the drawn path
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction *speed *Time.deltaTime;
        //checks if player is within range before moving
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance <= range)
        {
            rb.AddForce(force);
        }
        //moves our waypoint if we've reached it
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        //checks the direction the entitiy is going and flips the sprite accordingly
        if (rb.velocity.x >= 0.01f && !facingRight)
        {
            flip();
        }
        else if (rb.velocity.x <= -0.01f && facingRight)
        {
            flip();
        }
    }

    //flips the character sprite
    private void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0,180,0);
    }
}
