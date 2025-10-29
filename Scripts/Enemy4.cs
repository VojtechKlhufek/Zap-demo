using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : MovingEnemy
{
    void Start()
    {
        health = 3;
        speed = 4f;
        bulletLifeTime = 50f;

        fireStartTime = 0;
        
        playerGameObject = GameObject.Find("Player");
        
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (CanSeePlayer())
        {
            FollowPlayer(); 
            Shoot();
        }
        else Roam();
    }
}
