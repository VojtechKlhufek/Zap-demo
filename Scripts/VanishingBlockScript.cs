using UnityEngine;

public class VanishingBlockScript : MonoBehaviour
{
    private float startTime;
    private float reappearTime = 2f;

    private Collider2D collider;
    private SpriteRenderer renderer;

    private Color activeColor = new Color(0.47f, 0.47f, 0.47f, 1f);
    private Color inactiveColor = new Color(0.47f, 0.47f, 0.47f, 0.2f);

    void Start()
    {
        startTime = Time.time;
        collider = GetComponent<Collider2D>();
        renderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (!collider.enabled)
        {
            CheckTime();
        }
    }

    void CheckTime()
    {
        if (Time.time - startTime > reappearTime)
        {
            collider.enabled = true;
            renderer.color = activeColor;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bullet"))
        {
            collider.enabled = false;
            renderer.color = inactiveColor;
            startTime = Time.time;
        }
    }
}
