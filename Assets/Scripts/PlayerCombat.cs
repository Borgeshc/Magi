using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float attackFrequency;
    public GameObject primaryAttackEffect;
    public GameObject primaryAttackSpawn;

    public GameObject secondaryAttackEffect;
    public GameObject secondaryAttackSpawn;

    Transform target;

    bool attacking;

    CharacterAnimator anim;

    private void Start()
    {
        anim = GetComponent<CharacterAnimator>();
    }

    public void Attack(bool isPrimaryAttack, Transform _target)
    {
        if(!attacking)
        {
            attacking = true;
            target = _target;

            if (isPrimaryAttack)
                anim.PrimaryAttack();
            else
                anim.SecondaryAttack();

            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(attackFrequency);
        PlayerController.canMove = true;
        attacking = false;
    }

    public void PrimaryAttack()
    {
        PlayerController.canMove = false;
        GameObject projectile = Instantiate(primaryAttackEffect, primaryAttackSpawn.transform.position, transform.rotation) as GameObject;
    }

    public void SecondaryAttack()
    {
        PlayerController.canMove = false;
        Instantiate(secondaryAttackEffect, secondaryAttackSpawn.transform.position, transform.rotation);
    }
}
