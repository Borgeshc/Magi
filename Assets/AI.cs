using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public Animator anim;
    NavMeshAgent agent;
    Transform target;

    int hurtLevel;
    
	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Target").transform;
        anim = GetComponent<Animator>();
	}
	
	void Update ()
    {
        agent.SetDestination(target.position);
	}

    public void HurtLevel(int amount)
    {
        hurtLevel += amount;
        anim.SetInteger("Hurt", hurtLevel);
    }
}
