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

    float randomAnimSpeed;

    float playerDistance;


    public void Enter(JH_AiAgent agent){
        originPosition = agent.transform.position;
        
        enemyAni = agent.anim;
        enemyTransform = agent.transform;
        nav = agent.navMeshAgent;

        speed_NonCombat = agent.config.roamingSpeed;
        moveRange = agent.config.roamingRange;

        randomAnimSpeed = Random.Range(0.5f,1.5f);

        agent.navMeshAgent.SetDestination(target);

        playerTarget = GameObject.Find("Player");
        
    }
    void Move_NonCombat(){
        if(targetDis <= 3f){
            onMove = false;
            nav.speed = speed_NonCombat;
        }
        if(!onMove){
            onMove = true;
            target = new Vector3(enemyTransform.position.x + Random.Range(-1 * moveRange, moveRange), 0, enemyTransform.position.z + Random.Range(-1 * moveRange, moveRange));
            Debug.Log(target);
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

            agent.anim.speed = randomAnimSpeed;

            playerDistance = Vector3.Distance(playerTarget.transform.position, agent.transform.position);
            Debug.Log(playerDistance);

            Move_NonCombat();

            if(playerDistance <=  agent.config.maxSightDistance){
                enemyAni.SetTrigger("Detact");
                agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
            }
    }

    public void Exit(JH_AiAgent agent){
        
    }
}

