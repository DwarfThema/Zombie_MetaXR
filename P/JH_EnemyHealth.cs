using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    

    JH_AiAgent agent;


    public float blinkIntensity = 0.6f;
    public float blinkDuration = 0.3f;
    float blinkTimer;

    SkinnedMeshRenderer skinnedMeshRenderer;
    SkinnedMeshRenderer skinnedMeshRendererHead;
    

    JH_EnemyHealth healthScript;

    public GameObject head;
    public GameObject body;
    public GameObject headBlink;

    private void Start() {
        healthScript = GetComponent<JH_EnemyHealth>();
        
        skinnedMeshRenderer =GetComponentInChildren<SkinnedMeshRenderer>();
        skinnedMeshRendererHead = headBlink.GetComponent<SkinnedMeshRenderer>();

        agent = GetComponent<JH_AiAgent>();
        currentHealth = maxHealth;

        JH_isHeadHit headHit = head.GetComponent<JH_isHeadHit>();
        headHit.health = this;

        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach(var rigidBody in rigidBodies){
            JH_HitBox hitBox = rigidBody.gameObject.AddComponent<JH_HitBox>();
            hitBox.health = this;
        }
    }

    public void TakeDamage(float amount, Vector3 direction){
        currentHealth -= amount;
        if(currentHealth <= 0.0f){
            
            Die(direction);
        }

        blinkTimer = blinkDuration;
    }

    public void Die(Vector3 direction){
        JH_AiDeathState deathState = agent.stateMachine.GetState(AiStateId.Death) as JH_AiDeathState;
        deathState.direction = direction;
        agent.stateMachine.ChangeState(AiStateId.Death);
        //Destroy(gameObject, 5f);
    }



    private void Update() {
        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        float intensity = (lerp * blinkIntensity);

        skinnedMeshRenderer.material.SetColor("_EmissiveColor", (Color.white * intensity));
        if(skinnedMeshRendererHead){
        skinnedMeshRendererHead.material.SetColor("_EmissiveColor", (Color.white * intensity));
        }
        //skinnedMeshRenderer.material.color = Color.white * intensity;

        
     
    }



}
