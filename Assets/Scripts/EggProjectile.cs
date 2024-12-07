using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggProjectile : MonoBehaviour
{
    // Speed of the egg projectile set in inspector
    public float speed; 
    // Time before the egg destroys itself set in inspector
    public float lifetime; 

    private void Start()
    {
        // Destroy the egg after its lifetime
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Move the egg projectile downward
        transform.Translate(Vector2.down* speed * Time.deltaTime);
    }

}
