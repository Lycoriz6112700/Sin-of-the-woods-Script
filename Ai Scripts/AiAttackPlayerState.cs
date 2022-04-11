using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAttackPlayerState : AiState
{

    public AiStateId GetId(){
        return AiStateId.AttackPlayer;
    }

    public void Enter(AiAgent agent){
        
    }

    public void Update(AiAgent agent){
        Vector3 direction = (agent.playerTransform.position - agent.navMeshAgent.destination);
        agent.navMeshAgent.destination = agent.playerTransform.position;
        if(direction.sqrMagnitude >= 8.0f){
            agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
        }
    }

    public void Exit(AiAgent agent){

    }
}
