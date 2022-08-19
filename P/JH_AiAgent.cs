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

        float random = Random.Range(0,1);
        if(random == 1 ){
            stateMachine.ChangeState(initialState1);
        }else{
            stateMachine.ChangeState(initialState2);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
