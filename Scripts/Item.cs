using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;

public class Item : MonoBehaviour
{
    private enum Effect
    {
        FireRate,
        BulletLife,
        Bounciness,
        Health,
        ShotSpeed
    }

    private readonly Dictionary<Effect, Color> colorMap = new()
    {
        {Effect.FireRate, Color.yellow},
        {Effect.BulletLife, Color.blue},
        {Effect.Bounciness, Color.green},
        {Effect.Health, Color.red},
        {Effect.ShotSpeed, Color.cyan},
    };

    private Effect effect;

    private void Start()
    {
        effect = PickRandomEffect();
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = colorMap[effect];
    }

    private Effect PickRandomEffect()
    {
        return (Effect)Random.Range(0, (int)Enum.GetValues(typeof(Effect)).Cast<Effect>().Max());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject hitGameObject = collision.gameObject;
        if (hitGameObject.CompareTag("Player"))
        {
            Player playerScript = hitGameObject.GetComponent<Player>();
            switch (effect)
            {
                case (Effect.FireRate):
                    playerScript.IncreaseFireRate(0.2f);
                    break;
                case (Effect.BulletLife):
                    playerScript.IncreaseBulletLifeTime(0.5f);
                    break;
                case (Effect.Bounciness):
                    playerScript.IncreaseBounciness(1);
                    break;
                case (Effect.Health):
                    playerScript.IncreaseHealth(1);
                    break;
                case (Effect.ShotSpeed):
                    playerScript.IncreaseShotSpeed(2.5f);
                    break;
            }
            Destroy(gameObject);
        }
    }
}
