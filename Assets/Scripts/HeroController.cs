using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HeroController : MonoBehaviour
{

    public Rigidbody2D rb2d;
    public float speed = 0.05f; 

	// Use this for initialization
	void Start ()
	{
	    rb2d = GetComponent<Rigidbody2D>(); 
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
	}
}
