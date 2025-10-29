using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float startTime;

    private float lifeTime;

    //Used for granting the shooter immunity to his own bullet
    private Owner owner;

    private int maxBounceCount;
    private int bounceCount = 0;
    
    public enum Owner
    {
        Player,
        Enemy
    }
    public void SetOwner(Owner owner)
    {
        this.owner = owner;
    }

    public void SetLifeTime(float lifeTime)
    {
        this.lifeTime = lifeTime;
    }

    public void SetMaxBounceCount(int maxBounceCount)
    {
        this.maxBounceCount = maxBounceCount;
    }
    
    void Start()
    {
        startTime = Time.time;
    }

    void FixedUpdate()
    {
        CheckLifeTime();

    }

    void CheckLifeTime()
    {
        if (Time.time - startTime > lifeTime)
        {
            Destroy(this.GameObject());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject hitGameObject = collision.gameObject;
        
        if ((hitGameObject.CompareTag("Enemy") && owner == Owner.Player) || 
            (hitGameObject.CompareTag("Player") && owner == Owner.Enemy))
        {
            hitGameObject.GetComponent<CharacterBase>().TakeDamage();
        }
    
        bounceCount++;
        if (bounceCount > maxBounceCount)
        {
            Destroy(this.GameObject());
        }
    }
}
