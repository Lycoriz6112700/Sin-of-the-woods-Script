using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMotion : Photon.MonoBehaviour
{
    public Transform playerTransform;
    UnityEngine.AI.NavMeshAgent agent;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = playerTransform.position;
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }
}
