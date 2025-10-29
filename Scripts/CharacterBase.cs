using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    protected int health;
    protected float speed = 7.5f;
    protected float jumpStrength = 15f;
    
    //Min time between each shot
    protected float fireRate = 1f;
    //Time for which a bullet shot by this character exists
    protected float bulletLifeTime = 0.5f;
    //Speed with which a bullet is shot by this character
    protected float shotSpeed = 5f;
    //Max amount of bounces of bullets shot by this character
    protected int maxBulletBounceCount = 0;

    //Timer used to measure the time between shots
    protected float fireStartTime;

    //Various raycast properties to check if the character is on the ground or next to a wall or a hole
    private Vector2 groundBoxCastSize = new Vector2(1f, 0.1f);
    protected Vector2 wallBoxCastSize = new Vector2(0.1f, 0.7f);
    protected Vector2 holeCheckCastSize = new Vector2(0.2f, 0.2f);
    protected float groundCastDistance = 0.3f;
    protected float sideCastDistance = 0.5f;
    protected float holeSideCheckCastDistance = 0.7f;
    
    [SerializeField] protected LayerMask groundLayer;
    protected Vector3 sideCastOffset = new Vector3(0f,0.2f);
    protected Vector3 holeCheckOffset = new Vector3(0f, -0.5f);
    
    protected Rigidbody2D rigidbody;
    
    [SerializeField] protected GameObject bullet;
    
    public enum Side
    {
        Right,
        Left
    }
    
    protected bool IsOnGround()
    {
        return (Physics2D.BoxCast(transform.position, groundBoxCastSize,
            0, -transform.up, groundCastDistance, groundLayer));
    }

    public virtual void TakeDamage()
    {
        if (health == 1)
        {
            Destroy(this.gameObject);
        }
        else health -= 1;
    }
}
