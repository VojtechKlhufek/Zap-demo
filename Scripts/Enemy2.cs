using UnityEngine;

public class Enemy2 : MovingEnemy
{
    void Start()
    {
        health = 3;
        speed = 4f;
        
        playerGameObject = GameObject.Find("Player");
        
        rigidbody = GetComponent<Rigidbody2D>();
        
        jumpCooldownStartTime = Time.time;
    }

    void FixedUpdate()
    {
        if (CanSeePlayer())
        {
            FollowPlayer(); 
        }
        else Roam();
    }
}
