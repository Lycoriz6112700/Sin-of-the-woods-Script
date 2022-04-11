using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiChasePlayerState : AiState
{
    float timer = 0.0f;

    public AiStateId GetId(){
        return AiStateId.ChasePlayer;
    }

    public void Enter(AiAgent agent){
        
    }

    public void Update(AiAgent agent){
        if(!agent.enabled){
            return;
        }
        
        timer -= Time.deltaTime;
        if(!agent.navMeshAgent.hasPath){
            agent.navMeshAgent.destination = agent.playerTransform.position;
        }

        if(timer < 0.0f){
            Vector3 direction = (agent.playerTransform.position - agent.navMeshAgent.destination);
            if(direction.sqrMagnitude > agent.config.maxDistance * agent.config.maxDistance){
                if(agent.navMeshAgent.pathStatus != UnityEngine.AI.NavMeshPathStatus.PathPartial){
                    agent.navMeshAgent.destination = agent.playerTransform.position;
                }
            }
            if(direction.sqrMagnitude > 15.0f){
            agent.stateMachine.ChangeState(AiStateId.FindPlayer);
             }
            if(direction.sqrMagnitude < 8.00f){
            agent.stateMachine.ChangeState(AiStateId.AttackPlayer);
            }
            timer = agent.config.maxTime;
        }
       
    }

    public void Exit(AiAgent agent){
    }

}
