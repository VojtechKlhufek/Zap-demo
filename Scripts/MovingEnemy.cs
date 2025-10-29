using UnityEngine;

public class MovingEnemy : EnemyBase
{
    protected Direction direction;
    protected float jumpCooldownStartTime;
    private float jumpCooldown = 0.2f;
    
    protected enum Direction
    {
        Right,
        Left
    }
    
    //The enemy tries to follow the player based on their position
    protected void FollowPlayer()
    {
        if (playerGameObject.transform.position.x - transform.position.x > 0.1f)
        {
            rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
        }
        else if (playerGameObject.transform.position.x - transform.position.x < -0.1f)
        {
            rigidbody.velocity = new Vector2(-speed, rigidbody.velocity.y);
        }
        else{rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);}

        if (playerGameObject.transform.position.y > this.gameObject.transform.position.y + 2f)
        {
            Jump();
        }
    }

    //The enemy moves in one direction and switches the direction when an obstacle occurs
    protected void Roam()
    {
        if (IsTouchingWall(Side.Right) || IsTouchingWall(Side.Left)
            || (IsNextToHole(Side.Left) && direction == Direction.Left)
            || (IsNextToHole(Side.Right) && direction == Direction.Right))
        {
            direction = direction == Direction.Right ? Direction.Left : Direction.Right;
        }
        
        if (direction == Direction.Right)
        {
            rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
        }
        else
        {
            rigidbody.velocity = new Vector2(-speed, rigidbody.velocity.y);
        }
        
    }
    
    private void Jump()
    {
        float newTime = Time.time;
        if(IsOnGround() && newTime - jumpCooldownStartTime > jumpCooldown  )
        {
            jumpCooldownStartTime = newTime;
            rigidbody.velocity = (new Vector2(rigidbody.velocity.x, jumpStrength));
        } 
    }
    
    //On collision enter and stay deals damage to the player
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject hitGameObject = collision.gameObject;
        if (hitGameObject.CompareTag("Player"))
        {
            hitGameObject.GetComponent<Player>().TakeDamage();
        }
        else if (hitGameObject.CompareTag("Enemy"))
        {
            direction = direction == Direction.Right ? Direction.Left : Direction.Right;
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        GameObject hitGameObject = collision.gameObject;
        if (hitGameObject.CompareTag("Player"))
        {
            hitGameObject.GetComponent<Player>().TakeDamage();
        }
    }
}

