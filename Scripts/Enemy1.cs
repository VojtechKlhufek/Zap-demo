using UnityEngine;

public class Enemy1 : MovingEnemy
{
    void Start()
    {
        health = 2;
        speed = 5f;
        playerGameObject = GameObject.Find("Player");
        rigidbody = GetComponent<Rigidbody2D>();
        
        jumpCooldownStartTime = Time.time;
    }

    void FixedUpdate()
    {
        Roam();
    }
}
