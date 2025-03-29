using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyShield))]
public class Enemy_4 : Enemy
{
    private EnemyShield[] allShields;
    private EnemyShield thisShield;

    void Start()
    {
        allShields = GetComponentsInChildren<EnemyShield>();
        thisShield = GetComponent<EnemyShield>();   
    }

    public override void Move()
    {
        
    }

    /// <summary>
    /// Enemy_4 Collisions are handled diffferently from other Enemy subclasses
    /// to enable protection by EnemyShields.
    // </summary>
    // <param name="coll"></param>

    void OnCollisionEnter(Collision coll)
    {
        GameObject otherGO = coll.gameObject;

        // Make sure this was hit by a ProjectileHero
        ProjectileHero p = otherGO.GetComponent<ProjectileHero>();
        if (p != null) {
            // Destroy the ProjectileHero regardless of bndCheck.isOnScreen
            Destroy(otherGO);

            // Only damage this Enemy if it's on screen
            if (bndCheck.isOnScreen) {
                // Find the GameObject of this Enemy_4 that was actually hit
                GameObject hitGO = coll.contacts[0].thisCollider.gameObject;
                if(hitGO == otherGO) {
                    hitGO = coll.contacts[0].otherCollider.gameObject;
                }

                // Get the damage amount from the Main WEAP_DICT.
                float dmg = Main.GET_WEAPON_DEFINITION(p.type).damageOnHit;

                // FInd the EnemyShield that was hit (if there was one)
                bool shieldFound = false;
                foreach (EnemyShield es in allShields) {
                    if (es.gameObject == hitGO) {
                        es.TakeDamage(dmg);
                        shieldFound = true;
                    }
                }
                if (!shieldFound) thisShield.TakeDamage(dmg);

                // If thisShield is still active, then it has not been destroyed
                if (thisShield.isActive) return;

                // This ship was destroyed so tell Main about it
                if (!calledShipDestroyed) {
                    Main.SHIP_DESTROYED(this);
                    calledShipDestroyed = true;
                }

                // Destroy this Enemy_4
                Destroy(gameObject);
            }
            else {
                Debug.Log("Enemy_4 hit by non-ProjectileHero: "+ otherGO.name);
            }
            


        }
    }
}
