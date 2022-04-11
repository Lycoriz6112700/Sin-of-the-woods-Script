using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiFindPlayerState : AiState
{
    public float walkRadius = 150;

    public AiStateId GetId(){
        return AiStateId.FindPlayer;
    }

    public void Enter(AiAgent agent){
        
    }

    public void Update(AiAgent agent){
        if(!agent.navMeshAgent.hasPath || agent.navMeshAgent.remainingDistance <= agent.navMeshAgent.stoppingDistance){
            Vector3 finalPosition = Vector3.zero;
            Vector3 randomPosition = Random.insideUnitSphere * walkRadius;
            randomPosition += agent.transform.position;
            if(UnityEngine.AI.NavMesh.SamplePosition(randomPosition, out UnityEngine.AI.NavMeshHit hit, walkRadius, 1)){
                finalPosition = hit.position;
            }
            agent.navMeshAgent.destination = finalPosition;
        }

        Vector3 playerDirection = agent.playerTransform.position - agent.transform.position;
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

    public void Exit(AiAgent agent){

    }
}