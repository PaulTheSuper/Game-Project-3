using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    public LineRenderer line_renderer;
    public bool ignores_triggers = true;
    private BoxCollider2D box_collider;

    private void Start()
    {
        box_collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        UpdateLine();
    }

    private void FixedUpdate()
    {
        UpdateLine();
        CheckLine();
    }

    private void UpdateLine()
    {
        box_collider.enabled = false;
        Physics2D.queriesHitTriggers = !ignores_triggers;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Player.GetPlayer().transform.position - transform.position, 100.0f);
        line_renderer.SetPosition(0, transform.position);
        line_renderer.SetPosition(1, hit.point);
        box_collider.enabled = true;
        Physics2D.queriesHitTriggers = true;
    }

    private void CheckLine()
    {
        box_collider.enabled = false;
        Physics2D.queriesHitTriggers = !ignores_triggers;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Player.GetPlayer().transform.position - transform.position, 100.0f);
        if(hit.collider.gameObject.Equals(Player.GetPlayer().gameObject))
        {
            Player.ResetLevel();
        }
        box_collider.enabled = true;
        Physics2D.queriesHitTriggers = true;
    }
}
