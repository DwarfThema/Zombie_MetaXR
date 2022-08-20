using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_HitBox : MonoBehaviour
{
    public JH_EnemyHealth health;

    public void OnRaycastHit(RayCastWeapon1 weapon, Vector3 direction){
        health.TakeDamage(weapon.damage, direction*5);

    }

    public void OnExplosionHit(Vector3 direction){
        health.TakeDamage(health.maxHealth, direction);
    }


}
