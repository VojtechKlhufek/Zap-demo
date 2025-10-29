using TMPro;
using UnityEngine;

public class Player : CharacterBase
{
    private float jumpCooldownStartTime;
    private float jumpCooldown = 0.2f;
    
    private float damageCooldownStartTime;
    private float damageCooldown = 0.5f;
    
    private bool hasJumped;

    [SerializeField] private TextMeshProUGUI healthText;

    private Transform weapon;
    private Transform weaponVisual;

    private LevelManager levelManagerScript;
    
    void Start()
    {
        bulletLifeTime = 1f;
        rigidbody = GetComponent<Rigidbody2D>();
        weapon = transform.GetChild(0);
        weaponVisual = weapon.GetChild(0);

        fireStartTime = 0;
        
        rigidbody.gravityScale = 4;
        jumpCooldownStartTime = Time.time;
        damageCooldownStartTime = Time.time;
        health = 5;
        healthText.text = health.ToString();

        levelManagerScript = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    void Update()
    {
        CheckJump();
        UpdateWeapon();
    }
    
    private void FixedUpdate()
    {
        CheckMovement();
    }

    private void CheckJump()
    {
        float newTime = Time.time;
        if(IsOnGround() && Input.GetKeyDown(KeyCode.W) && newTime - jumpCooldownStartTime > jumpCooldown)
        {
            hasJumped = true;
            jumpCooldownStartTime = newTime;
            rigidbody.velocity = (new Vector2(rigidbody.velocity.x, jumpStrength));
        } 
    }

    private void UpdateWeapon()
    {
        Vector3 weaponpos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousepos = Input.mousePosition;

        //The rotation angle of the weapon
        float angle = (Mathf.Atan2(weaponpos.y - mousepos.y,
            weaponpos.x - mousepos.x) * (180/Mathf.PI));
        
        weapon.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        
        float fireTime = Time.time;
        //Shoots a bullet
        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse0)) && fireTime - fireStartTime > fireRate)
        {
            fireStartTime = fireTime;
            
            Vector3 weaponVisualPosition = weaponVisual.position;
            Vector3 bulletSpawnPoint = weaponVisualPosition + 0.2f*weaponVisual.forward;
            Vector3 shootDir = weaponVisualPosition - weapon.position;
            shootDir.Normalize();
            
            GameObject bul = Instantiate(bullet, bulletSpawnPoint, weaponVisual.rotation);
            Bullet bulletScript = bul.GetComponent<Bullet>();
            Rigidbody2D bulrb = bul.GetComponent<Rigidbody2D>();
            
            bulletScript.SetOwner(Bullet.Owner.Player);
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), bul.GetComponent<Collider2D>());
            bulletScript.SetLifeTime(bulletLifeTime);
            bulletScript.SetMaxBounceCount(maxBulletBounceCount);
            
            bulrb.velocity = shootDir*shotSpeed;
                
        }
    }

    private void CheckMovement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rigidbody.velocity = new Vector3(speed, rigidbody.velocity.y);
        }
        else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            rigidbody.velocity = new Vector3(-speed, rigidbody.velocity.y);
        }
        else
        {
            rigidbody.velocity = new Vector3(0f, rigidbody.velocity.y);
        } 
    }

    public override void TakeDamage()
    {
        float currentTime = Time.time;
        if (currentTime - damageCooldownStartTime > damageCooldown)
        {
            if (health == 1)
            {
                levelManagerScript.EndGame();
            }
            else health -= 1;
            healthText.text = health.ToString();
            damageCooldownStartTime = currentTime;
        }
        
    }

    //Used by LevelManager to toggle off the ground after a jump
    public bool HasJumped()
    {
        bool result = hasJumped;
        hasJumped = false;
        return result;
    }

    //The bellow methods are used by items
    public void IncreaseBounciness(int count)
    {
        maxBulletBounceCount += count;
    }

    public void IncreaseHealth(int count)
    {
        health += count;
        healthText.text = health.ToString();
    }

    public void IncreaseBulletLifeTime(float count)
    {
        bulletLifeTime += count;
    }

    public void IncreaseShotSpeed(float count)
    {
        shotSpeed += count;
    }
    
    public void IncreaseFireRate(float count)
    {
        if (fireRate - count <= 0.05f)
        {
            fireRate = 0.05f;
        }
        else fireRate -= count;
    }
}
