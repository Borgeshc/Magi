using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 direction;

	void Update ()
    {
        transform.Rotate(direction * Time.deltaTime);
	}
}
