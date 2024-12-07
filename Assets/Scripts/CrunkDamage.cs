using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrunkDamage : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyProjectile"))
        {
            CrunkHealth crunkHealth = GetComponent<CrunkHealth>();
            if (crunkHealth != null)
            {
                // Projectiles cause 1 damage
                crunkHealth.TakeDamage(1); 
                // Destroy the projectile object after damaging Crunk
                Destroy(collision.gameObject); 
            }
        }
    }
}
