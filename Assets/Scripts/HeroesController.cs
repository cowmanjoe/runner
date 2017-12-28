using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroesController : MonoBehaviour
{
    public delegate void DeathAction();
    public event DeathAction OnDeath;

    public float speed = 0.05f;
    public HeroController bottomHero;
    public HeroController topHero;


    private void OnEnable()
    {
        bottomHero.OnDeath += Death;
        topHero.OnDeath += Death;
    }

    private void OnDisable()
    {
        bottomHero.OnDeath -= Death;
        topHero.OnDeath -= Death;
    }

    void Death()
    {
        transform.position = new Vector3(0, 0, transform.position.z);
        topHero.transform.position = topHero.initialPosition;
        bottomHero.transform.position = bottomHero.initialPosition;

        OnDeath();
    }

    // Update is called once per frame
    void FixedUpdate () {
		transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
    }
}
