using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHealth : MonoBehaviour
{
    public List<GameObject> cubes;
    public GameObject title;
    public Animator killCounter;

    public int maxHealth;

    int health;
    bool isDead;
    Animator anim;

    private void Start()
    {
        health = maxHealth;
        anim = GetComponent<Animator>();
    }

    public void TookDamage()
    {
        health--;

        if (health < 0 && !isDead)
        {
            isDead = true;
            StartCoroutine(Died());
        }
        else
        anim.SetTrigger("Damaged");
    }

    IEnumerator Died()
    {
        yield return new WaitForSeconds(1);
        title.SetActive(true);
        killCounter.enabled = true;

        yield return new WaitForSeconds(1);
        Time.timeScale = 0;
    }

    public void ThrowCube()
    {
        int cube = Random.Range(0, cubes.Count);
        cubes[cube].transform.parent = null;
        cubes[cube].GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1, 1), 1, Random.Range(-1, 1)) * 250);
        cubes[cube].GetComponent<Rigidbody>().useGravity = true;

        cubes.Remove(cubes[cube]);
    }
}
