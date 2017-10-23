﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHealth : MonoBehaviour
{
    public List<GameObject> cubes;

    public int maxHealth;

    int health;
    Animator anim;

    private void Start()
    {
        health = maxHealth;
        anim = GetComponent<Animator>();
    }

    public void TookDamage()
    {
        health--;

        if (health < 0) return;
        anim.SetTrigger("Damaged");
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
