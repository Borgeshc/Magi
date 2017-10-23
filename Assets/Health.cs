using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    public int maxHeath;
    public float destroyAfter;
    public GameObject blood;
    public GameObject explosion;
    public Animator anim;
    public GameObject hitEffect;

    int health;
    bool isDead;

    AI ai;
    NavMeshAgent agent;
    SpawnManager spawnManager;
    Collider col;

    private void Start()
    {
        health = maxHeath;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        spawnManager = GameObject.Find("GameManager").GetComponent<SpawnManager>();
        col = GetComponent<Collider>();
        ai = GetComponent<AI>();
    }

    public void TookDamage(int damage)
    {
        if (isDead) return;

        health -= damage;
        ai.HurtLevel(damage);
        anim.SetTrigger("Hurt");
        Instantiate(explosion, transform.position, transform.rotation);
       // Instantiate(hitEffect, transform.position, Quaternion.identity);

        if(health <= 0)
        {
            Died();
        }
    }

    void Died()
    {
        isDead = true;
        col.enabled = false;
        Instantiate(blood, transform.position, transform.rotation);
        spawnManager.RemoveEnemy(gameObject);
        agent.isStopped = true;
        anim.SetInteger("Died", Random.Range(1, 3));
        Destroy(gameObject, destroyAfter);
    }
}
