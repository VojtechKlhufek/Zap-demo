using UnityEngine;

public class Enemy3 : EnemyBase
{
    
    void Start()
    {
        health = 3;
        speed = 4f;
        bulletLifeTime = 50f;
        fireRate = 0.4f;
        
        fireStartTime = 0;
        
        playerGameObject = GameObject.Find("Player");
        
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(CanSeePlayer()) Shoot();
    }
}
