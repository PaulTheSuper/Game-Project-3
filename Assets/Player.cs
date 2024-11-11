using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player instance;
    private BoxCollider2D box_collider;
    private Rigidbody2D rb;
    private float speed = 10f;
    public GameObject projectile_prefab;

    private void Start()
    {
        instance = this;
        box_collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
        if(Input.GetMouseButtonDown(0))
        {
            // Subtracts to make (0, 0) the center of the screen which is the player
            Vector3 player_to_mouse = Input.mousePosition;
            player_to_mouse.x -= Screen.width / 2;
            player_to_mouse.y -= Screen.height / 2;
            player_to_mouse.Normalize();
            GameObject obj = Instantiate(projectile_prefab);
            obj.transform.position = transform.position + player_to_mouse * 0.75f;
            Projectile projectile = obj.GetComponent<Projectile>();
            projectile.direction = player_to_mouse;
            projectile.speed = 24;
        }
    }

    private void FixedUpdate()
    {
        Vector2 move_direction = new Vector2();
        if(Input.GetKey(KeyCode.W))
        {
            move_direction.y += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            move_direction.x -= 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            move_direction.y -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move_direction.x += 1;
        }
        move_direction.Normalize();
        Vector2 new_position = rb.position;
        Vector2 movement = move_direction * speed * Time.fixedDeltaTime;
        // Pretty easily extendable to 3d which is cool
        box_collider.enabled = false;
        Physics2D.queriesHitTriggers = false;
        while (movement.magnitude > 0)
        {
            // Move at an exact floating point number so we have consistent collision results (attempt at not being stuck in walls)
            float step_move_x = Mathf.Clamp(movement.x, -0.0625f, 0.0625f);
            float step_move_y = Mathf.Clamp(movement.y, -0.0625f, 0.0625f);
            if (Mathf.Abs(step_move_x) == 0.0625f && Physics2D.OverlapBox(new_position + new Vector2(step_move_x, 0), box_collider.size, 0) == null)
            {
                new_position.x += step_move_x;
            }
            if (Mathf.Abs(step_move_y) == 0.0625f && Physics2D.OverlapBox(new_position + new Vector2(0, step_move_y), box_collider.size, 0) == null)
            {
                new_position.y += step_move_y;
            }
            movement.x -= step_move_x;
            movement.y -= step_move_y;
        }
        box_collider.enabled = true;
        Physics2D.queriesHitTriggers = true;
        rb.MovePosition(new_position);
    }

    public static Player GetPlayer()
    {
        return Player.instance;
    }

}
