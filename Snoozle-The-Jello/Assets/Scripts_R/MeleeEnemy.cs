﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Character
{
    public GameObject player;
    public Rigidbody2D rb;
    [SerializeField]
    public bool isFacingRight;
    public bool isSeeking;

    public float Damage
    { get { return damage; } }

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        MaxHealth = 10.0f;
        health = MaxHealth;
        damage = 5.0f;
        rb = gameObject.GetComponent<Rigidbody2D>();
        isSeeking = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    // leacing this blank b/c not worth
    public override void Shoot()
    {
        
    }

    /// <summary>
    /// decides whether to Seek or Wander
    /// </summary>
    public override void Move()
    {
        if (XDistBetween(player) <= 4.0f && YDistBetween(player) <= 0.75f)
        {
            Seek(player);
            isSeeking = true;
            Debug.Log("Seeking");
        }
        else
        {
            Wander();
            isSeeking = false;
            Debug.Log("Wandering");
        }
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, 1.5f);
    }

    /// <summary>
    /// Returns the x-distance between two objects in a float
    /// </summary>
    /// <param name="otherObj"></param>
    /// <returns></returns>
    private float XDistBetween(GameObject otherObj)
    {
        return otherObj.transform.position.x - gameObject.transform.position.x;
    }

    /// <summary>
    /// Returns the y-distance between two objects in a float
    /// </summary>
    /// <param name="otherObj"></param>
    /// <returns></returns>
    private float YDistBetween(GameObject otherObj)
    {
        return otherObj.transform.position.y - gameObject.transform.position.y;
    }

    /// <summary>
    /// Moves the enemy towards the player character
    /// Can tweek the forces to make ot slower or whatrever we need
    /// </summary>
    /// <param name="targetObj">the object this enemy is targeting </param>
    private void Seek(GameObject targetObj)
    {
        // gameObject.transform.LookAt(targetObj.transform);
        if (XDistBetween(player) > 0)
        {
            rb.AddForce(new Vector2(1.0f, 0));
        }
        else if (XDistBetween(player) <0 )
        {
            rb.AddForce(new Vector2(-1.0f, 0));
        }
    }

    /// <summary>
    /// Causes the character to move in one direction. Works with OnCollisionEnter2D to turn around when it hits a pathway object
    /// </summary>
    private void Wander()
    {
        if (isFacingRight)
        {
            rb.AddForce(new Vector2(1.0f, 0));
        }
        else
        {
            rb.AddForce(new Vector2(-1.0f, 0));
        }
    }

    /// <summary>
    /// Resolves collisions on enter
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            // This is where i could call takedamage on the player character
            // collision.gameObject.GetComponent<>().TakeDamage();
            // Debug.Log("Hit player");
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pathway" && isSeeking == false)
        {
            //gameObject.transform.forward = new Vector3(-gameObject.transform.forward.x, 0.0f, 0.0f);
            isFacingRight = !isFacingRight;
        }
    }
}
