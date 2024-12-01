using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player instance;
    public int current_level = 1;
    private BoxCollider2D box_collider;
    private Rigidbody2D rb;
    public GameObject projectile_prefab;
    public AudioClip shoot_audio;
    public AudioClip advance_audio;
    public AudioClip reset_audio;
    public AudioClip toggle_audio;

    private void Start()
    {
        instance = this;
        box_collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        ResetLevelNoSound();
        LevelIntro.DisplayLevelText();
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
            AudioSource audio = Player.GetPlayer().gameObject.GetComponent<AudioSource>();
            audio.PlayOneShot(Player.GetPlayer().shoot_audio);
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            if(Input.GetKeyDown(KeyCode.O))
            {
                current_level = Mathf.Max(1, current_level - 1);
                ResetLevel();
                LevelIntro.DisplayLevelText();
            }
            else if(Input.GetKeyDown(KeyCode.P))
            {
                AdvanceLevel();
            }
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
        float speed = 10;
        float friction_modifier = 0;
        // Platform movement
        Collider2D[] overlap_results = Physics2D.OverlapBoxAll(transform.position, box_collider.size, 0);
        bool on_platform = false;
        bool on_ground = false;
        bool on_rotating = false;
        for (int i = 0; i < overlap_results.Length; i++)
        {
            Platform platform = overlap_results[i].GetComponent<Platform>();
            if (platform != null && !on_platform)
            {
                on_platform = true;
                if (platform.carries_player)
                {
                    new_position += platform.GetMovement();
                }
            }
            Ground ground = overlap_results[i].GetComponent<Ground>();
            if(ground != null && !on_ground)
            {
                speed = ground.GetSpeed();
                friction_modifier = ground.GetFrictionModifier();
                on_ground = true;
            }
            Rotating rotating = overlap_results[i].GetComponent<Rotating>();
            if (rotating != null && !on_rotating)
            {
                Vector3 difference = transform.position - rotating.transform.position;
                difference.z = 0;
                float radius = difference.magnitude;
                float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                angle += rotating.rotation_per_second * Time.fixedDeltaTime;
                Vector2 rotating_new_position = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * radius;
                rotating_new_position.x += rotating.transform.position.x;
                rotating_new_position.y += rotating.transform.position.y;
                Vector2 rotating_movement = rotating_new_position - rb.position;
                new_position += rotating_movement;
                on_rotating = true;
            }
        }
        // Different speed and friction depending on surface
        rb.velocity += move_direction * speed;
        Vector2 movement = rb.velocity * Time.fixedDeltaTime;
        // Friction (0 velocity retained by default)
        rb.velocity *= friction_modifier;
        // Pretty easily extendable to 3d which is cool
        box_collider.enabled = false;
        Physics2D.queriesHitTriggers = false;
        while (movement.magnitude > 0)
        {
            // Move at an exact floating point number so we have consistent collision results 1/2^6 (attempt at not being stuck in walls)
            float precision = 0.015625f;
            float step_move_x = Mathf.Clamp(movement.x, -precision, precision);
            float step_move_y = Mathf.Clamp(movement.y, -precision, precision);
            if (Mathf.Abs(step_move_x) == precision && Physics2D.OverlapBox(new_position + new Vector2(step_move_x, 0), box_collider.size, 0) == null)
            {
                new_position.x += step_move_x;
            }
            if (Mathf.Abs(step_move_y) == precision && Physics2D.OverlapBox(new_position + new Vector2(0, step_move_y), box_collider.size, 0) == null)
            {
                new_position.y += step_move_y;
            }
            movement.x -= step_move_x;
            movement.y -= step_move_y;
        }
        box_collider.enabled = true;
        Physics2D.queriesHitTriggers = true;
        rb.MovePosition(new_position);
        box_collider.enabled = false;
        Physics2D.queriesHitTriggers = false;
        // Reset if the player is stuck in a wall
        if (Physics2D.OverlapBox(transform.position, box_collider.size, 0) != null)
        {
            Debug.Log("Stuck in a wall, resetting level");
            ResetLevel();
        }
        box_collider.enabled = true;
        Physics2D.queriesHitTriggers = true;
        // Reset if the player has no floor beneath them
        if (!on_platform && !on_ground && MainTilemap.GetMainTilemap().GetTile(Vector3Int.FloorToInt(transform.position)) == null)
        {
            ResetLevel();
        }
    }

    public static void AddVelocity(Vector2 velocity)
    {
        Player.instance.rb.velocity += velocity;
    }

    public static void AddPosition(Vector3 position)
    {
        Player.GetPlayer().transform.position += position;
    }

    // For UnityEvent since Vectors aren't supported
    public static void AddXPosition(float x_position)
    {
        AddPosition(new Vector3(x_position, 0, 0));
    }

    public static void AddYPosition(float y_position)
    {
        AddPosition(new Vector3(0, y_position, 0));
    }

    public static Player GetPlayer()
    {
        return Player.instance;
    }

    public static void ResetLevel()
    {
        if (Player.instance.current_level == -1)
        {
            Player.instance.current_level = Level.levels.Count;
        }
        Player.instance.transform.position = Level.levels[Player.instance.current_level].GetSpawnLocation();
        AudioSource audio = Player.GetPlayer().gameObject.GetComponent<AudioSource>();
        audio.PlayOneShot(Player.GetPlayer().reset_audio);
    }

    public static void ResetLevelNoSound()
    {
        if (Player.instance.current_level == -1)
        {
            Player.instance.current_level = Level.levels.Count;
        }
        Player.instance.transform.position = Level.levels[Player.instance.current_level].GetSpawnLocation();
    }

    public static void AdvanceLevel()
    {
        AudioSource audio = Player.GetPlayer().gameObject.GetComponent<AudioSource>();
        audio.PlayOneShot(Player.GetPlayer().advance_audio);
        Player.instance.current_level = Mathf.Min(Player.instance.current_level + 1, Level.levels.Count);
        ResetLevelNoSound();
        LevelIntro.DisplayLevelText();
    }

}
