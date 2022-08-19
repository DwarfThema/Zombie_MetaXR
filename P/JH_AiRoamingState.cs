using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JH_AiRoamingState : JH_AiState
{
    public Vector3 direction;

    public AiStateId GetId(){
        return AiStateId.Roaming;
    }

    public float speed_NonCombat;
    public int moveRange;

    bool onMove;
    Vector3 originPosition;
    float originDis;
    Vector3 target;
    float targetDis;

    Animator enemyAni;
    Transform enemyTransform;
    NavMeshAgent nav;

    GameObject playerTarget;


    public void Enter(JH_AiAgent agent){
        originPosition = agent.transform.position;
        //agent.StartCoroutine("EnemyAI");
        enemyAni = agent.anim;
        enemyTransform = agent.transform;
        nav = agent.navMeshAgent;

        speed_NonCombat = agent.config.roamingSpeed;
        moveRange = agent.config.roamingRange;

        agent.navMeshAgent.SetDestination(target);

        playerTarget = GameObject.FindGameObjectWithTag("Player");
        
    }
    void Move_NonCombat(){
        if(targetDis <= 3){
            onMove = false;
            nav.speed = speed_NonCombat;
        }
        if(!onMove){
            onMove = true;
            target = new Vector3(enemyTransform.position.x + Random.Range(-1 * moveRange, moveRange), 0, enemyTransform.position.z + Random.Range(-1 * moveRange, moveRange));
            nav.SetDestination(target);
        }
    if(originDis>=moveRange){
        onMove = true;
        target = originPosition;
        nav.SetDestination(target);
    }

    }

    public void Update(JH_AiAgent agent){
            originDis = (originPosition - enemyTransform.position).magnitude;
            targetDis = (target - enemyTransform.position).magnitude;

            Move_NonCombat();

            float playerDistance = Vector3.Distance(agent.transform.position, playerTarget.transform.position);

            if(playerDistance <=  agent.config.maxSightDistance){
                enemyAni.SetTrigger("Detact");
                agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
            }
    }

    public void Exit(JH_AiAgent agent){
        
    }
}

