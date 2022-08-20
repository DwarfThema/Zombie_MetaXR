using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_AiChasePlayerState : JH_AiState
{
    float timer = 0.0f;
    float randomAnim;
    float randomAnimSpeed;

    public AiStateId GetId(){
        return AiStateId.ChasePlayer;
    }

    public void Enter(JH_AiAgent agent){
        
        randomAnim = Random.Range(0,20);
        randomAnimSpeed = Random.Range(0.5f,1.5f);
    }

    public void Update(JH_AiAgent agent){

        
        if(agent.navMeshAgent.hasPath){
            agent.anim.SetFloat("Speed", agent.navMeshAgent.velocity.magnitude);
        }else{
            agent.anim.SetFloat("Speed", 0);
        }
        
        if(!agent.enabled){
            return;
        }
        
        timer -= Time.deltaTime;
        if(!agent.navMeshAgent.hasPath){
            agent.navMeshAgent.destination = agent.gameObject.transform.position;
        }
        
        agent.anim.speed = randomAnimSpeed;


        if(randomAnim > 5){
            agent.navMeshAgent.speed = 3.5f;
        }else{
            agent.navMeshAgent.speed = 0.4f;
        }

        if(timer < 0.0f){
            if(agent.playerObject){
                float sqrDistance = (agent.playerObject.transform.position - agent.navMeshAgent.destination).sqrMagnitude;
                if(sqrDistance > agent.config.maxDistance * agent.config.maxDistance){
                    agent.navMeshAgent.destination = agent.playerObject.transform.position;
                }
            }
            timer = agent.config.maxTime;
        }
    }

    public void Exit(JH_AiAgent agent){
        
    }


}
