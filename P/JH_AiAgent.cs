using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JH_AiAgent : MonoBehaviour
{
    public JH_AiStateMachine stateMachine;
    public AiStateId initialState1;
    public AiStateId initialState2;
    public NavMeshAgent navMeshAgent;
    public JH_AiAgentConfig config;
    public JH_Ragdoll ragdoll;
    public GameObject playerObject;
    public Animator anim;
    public GameObject agentObject;

    // Start is called before the first frame update
    void Start()
    {
        agentObject = gameObject;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        ragdoll = GetComponent<JH_Ragdoll>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        stateMachine = new JH_AiStateMachine(this);
        stateMachine.RegisterState(new JH_AiChasePlayerState());
        stateMachine.RegisterState(new JH_AiDeathState());
        stateMachine.RegisterState(new JH_AiIdleState());
        stateMachine.RegisterState(new JH_AiRoamingState());

        int randomRotate = Random.Range(0,360);
        gameObject.transform.rotation = Random.rotation;

        int random = Random.Range(0,4);
        if(random == 1 ){
            stateMachine.ChangeState(initialState1);
            if(initialState1 == AiStateId.ChasePlayer){
                anim.SetTrigger("Detact");
                stateMachine.ChangeState(AiStateId.ChasePlayer);
            }
        }else{
            stateMachine.ChangeState(initialState2);
            if(initialState1 == AiStateId.ChasePlayer){
                anim.SetTrigger("Detact");
                stateMachine.ChangeState(AiStateId.ChasePlayer);
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
