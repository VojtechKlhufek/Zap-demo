using Unity.Mathematics;
using UnityEngine;

public class EnemyBase : CharacterBase
{
    protected GameObject playerGameObject;
    
    [SerializeField] protected LayerMask wallLayer;
    [SerializeField] private GameObject itemPrefab;
    
    System.Random rng = new();

    protected bool IsNextToHole(Side side)
    {
        int dirMultiplier = side == Side.Left ? -1 : 1;
        return !Physics2D.BoxCast(transform.position + holeCheckOffset, holeCheckCastSize, 
            0,transform.right * dirMultiplier, holeSideCheckCastDistance, groundLayer);
    }

    protected bool IsTouchingWall(Side side)
    {
        int dirMultiplier = side == Side.Left ? -1 :  1;
        
        return (Physics2D.BoxCast(transform.position + sideCastOffset, wallBoxCastSize,
            0, transform.right * dirMultiplier, sideCastDistance, wallLayer));
    }
    
    //Casts a ray and checks if the player is the first thing it hits
    protected bool CanSeePlayer()
    {
        Vector2 direction = (playerGameObject.transform.position - transform.position).normalized;
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int bulletLayer = LayerMask.NameToLayer("Bullet");

        //creates a LayerMask with all layers except Enemy and Bullet
        int layerMask = ~(1 << enemyLayer | 1 << bulletLayer);
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 50, layerMask);

        if (hit.collider.CompareTag("Player"))
        {
            return true; 
        }
        return false; 
    }
    protected void Shoot()
    {
        float fireTime = Time.time;
        if (fireTime - fireStartTime > fireRate)
        {
            fireStartTime = fireTime;
            
            Vector3 shootDir = playerGameObject.transform.position - transform.position;
            shootDir.Normalize();
            
            GameObject bul = Instantiate(bullet, transform.position, quaternion.identity);
            Bullet bulletScript = bul.GetComponent<Bullet>();
            Rigidbody2D bulrb = bul.GetComponent<Rigidbody2D>();
            
            bulletScript.SetOwner(Bullet.Owner.Enemy);
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), bul.GetComponent<Collider2D>());
            bulletScript.SetLifeTime(bulletLifeTime);
            
            bulrb.velocity = shootDir*shotSpeed;
        }
    }

    void SpawnItem()
    {
        if (rng.Next()%2==1)
        {
            Instantiate(itemPrefab, transform.position, quaternion.identity);
        }
    }

    public override void TakeDamage()
    {
        if (health == 1)
        {
            SpawnItem();
            Destroy(gameObject);
        }
        else health -= 1;
    }
}
