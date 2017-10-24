using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    public float movementSmoothTime;
    public int numberOfPrimaryAttacks;
    public int numberOfSecondaryAttacks;
    public int numberOfHits;
    public bool isPlayer;

    Animator anim;
    NavMeshAgent agent;
    
    int randomAttack;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (agent.velocity.magnitude == 0)
            anim.SetBool("IsIdle", true);
        else
            anim.SetBool("IsIdle", false);
    }

    public void PrimaryAttack()
    {
        if (!isPlayer) return;
        randomAttack = Random.Range(0, numberOfPrimaryAttacks);
        anim.SetTrigger("PrimaryAttack" + (randomAttack + 1));
    }

    public void SecondaryAttack()
    {
        if (!isPlayer) return;
        int randomAttack = Random.Range(0, numberOfSecondaryAttacks);
        anim.SetTrigger("SecondaryAttack" + (randomAttack + 1));
    }

    public void Hit()
    {
        if (isPlayer) return;
        int randomHit = Random.Range(0, numberOfHits);
        anim.SetTrigger("Hit" + (randomHit + 1));
    }

    public void Died()
    {
        if (isPlayer) return;
        anim.SetBool("Died", true);
    }

    public void Respawn()
    {
        if (isPlayer) return;
        anim.SetBool("Died", false);
    }
}
