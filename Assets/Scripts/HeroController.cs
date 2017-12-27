using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HeroController : MonoBehaviour
{
    public delegate void DeathAction();
    public event DeathAction OnDeath;

    public Rigidbody2D rb2d;
    public float speed = 0.05f;
    public SimplePlatformController BottomHero;
    public SimplePlatformController TopHero; 

	// Use this for initialization
	void Start ()
	{
	    rb2d = GetComponent<Rigidbody2D>(); 
	}

    private void OnEnable()
    {
        BottomHero.OnDeath += Death;
        TopHero.OnDeath += Death;
    }

    private void OnDisable()
    {
        BottomHero.OnDeath -= Death;
        TopHero.OnDeath -= Death;
    }

    void Death()
    {
        transform.position = new Vector3(0, 0, transform.position.z);
        OnDeath(); 
    }

    // Update is called once per frame
    void FixedUpdate () {
		transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
	}
}
