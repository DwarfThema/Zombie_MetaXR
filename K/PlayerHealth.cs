using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    JH_Ragdoll ragdoll;
    protected override void OnStart()
    {
        ragdoll = GetComponent<JH_Ragdoll>();
      
    }
    protected override void OnDeath(Vector3 direction)
    {
        ragdoll.ActivateRagdoll();
    }
    protected override void OnDamage(Vector3 direction)
    {

    }
    protected override void OnUpdate(float intensity)
    {
       
    }
}
