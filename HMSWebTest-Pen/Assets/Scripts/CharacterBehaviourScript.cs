using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class CharacterBehaviourScript : MonoBehaviour, IPointerClickHandler
{
    //public Transform[] targetArray;
    //public Transform target1, target2;
    Transform target;
    NavMeshAgent agent;
    Animator animator;
    public GameObject gameController;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.enabled = false;
        target = transform;
    }

    public void setTarget(Transform targ)
    {
        target = targ;
        animator.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    // Done
                    //Debug.Log("reached");
                    //target = target2;
                    gameController.SendMessage("checkAvatarReachTarget");
                    animator.enabled = false;
                }
            }
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("click!");
        throw new NotImplementedException();
    }
}
