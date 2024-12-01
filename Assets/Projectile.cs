using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D box_collider;
    public Vector2 direction = new Vector2(0, 0);
    public float speed = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box_collider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * speed;
        if((Player.GetPlayer().transform.position - transform.position).magnitude > 300)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.Equals(Player.GetPlayer().gameObject))
        {
            return;
        }
        if (collision.gameObject.GetComponent<Projectile>() != null)
        {
            return;
        }
        if(collision.gameObject.GetComponent<IgnoreProjectile>() != null)
        {
            return;
        }
        Destroy(gameObject);
    }

}
