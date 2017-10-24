using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public Animator anim;
    NavMeshAgent agent;
    Transform target;
    TargetHealth targetHealth;

    int hurtLevel;
    bool attacking;
    
	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Target").transform;
        targetHealth = GameObject.Find("TargetObject").GetComponent<TargetHealth>();
        anim = GetComponent<Animator>();
        anim.SetBool("IsWalking", true);
	}
	
	void Update ()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if(distance > 2f)
            agent.SetDestination(target.position);
        else
        {
            agent.speed = 0;
            agent.velocity = Vector3.zero;
            anim.SetBool("IsWalking", false);
            if(!attacking)
            {
                attacking = true;
                StartCoroutine(Attack());
            }
        }
	}

    IEnumerator Attack()
    {
        anim.SetTrigger("Attack");
        targetHealth.TookDamage();
        yield return new WaitForSeconds(2);
        attacking = false;
    }

    public void HurtLevel(int amount)
    {
        hurtLevel += amount;
        anim.SetInteger("Hurt", hurtLevel);
    }
}
