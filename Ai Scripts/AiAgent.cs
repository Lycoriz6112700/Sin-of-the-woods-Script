using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : Photon.MonoBehaviour
{
     public AiStateMachine stateMachine;
     public AiStateId initialState;
     public NavMeshAgent navMeshAgent;
     public AiAgentConfig config;
     public Transform playerTransform;
     public AiSensor sensor;
     float timer = 0.0f;

    private void Awake(){
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        sensor = GetComponent<AiSensor>();
        
        stateMachine = new AiStateMachine(this);
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.RegisterState(new AiAttackPlayerState());
        stateMachine.RegisterState(new AiFindPlayerState());
        stateMachine.ChangeState(initialState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
        timer -= Time.deltaTime;
        if(timer < 0.0f){
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            timer = config.maxTime;
        }
    }

}
