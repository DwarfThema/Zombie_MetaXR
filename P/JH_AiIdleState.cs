using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_AiIdleState : JH_AiState
{
    // Start is called before the first frame update
    public AiStateId GetId(){
        return AiStateId.Idle;
    }

    public void Enter(JH_AiAgent agent){
    }

    public void Update(JH_AiAgent agent){
        Vector3 playerDirection = agent.playerObject.transform.position - agent.transform.position;
        if(playerDirection.magnitude > agent.config.maxSightDistance){
            return;   
        }

        Vector3 agentDirection = agent.transform.forward;
        playerDirection.Normalize();

        float dotProduct = Vector3.Dot(playerDirection, agentDirection);
        if(dotProduct > 0.0f){
            agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
        }
    }

    public void Exit(JH_AiAgent agent){
        
    }
}
