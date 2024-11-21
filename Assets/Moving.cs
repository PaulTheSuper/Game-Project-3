using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public List<Vector2> checkpoints_relative = new List<Vector2>();
    public List<float> checkpoints_speed_multiplier = new List<float>();
    private Rigidbody2D rb;
    private int current_checkpoint = 0;
    private float time_moving = 0;
    private float distance_difference = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CalculateDistanceDifference();
        for (int i = 0; i < checkpoints_relative.Count; i++)
        {
            checkpoints_relative[i] = new Vector2(checkpoints_relative[i].x + transform.position.x, checkpoints_relative[i].y + transform.position.y);
        }
        if (checkpoints_relative[current_checkpoint].Equals(transform.position))
        {
            current_checkpoint++;
            CalculateDistanceDifference();
        }
    }

    private void FixedUpdate()
    {
        float completion_amount = Mathf.Min(1, (GetSpeedMultiplier() * time_moving) / distance_difference);
        Vector2 new_position = Vector2.Lerp(checkpoints_relative[GetPreviousCheckpoint()], checkpoints_relative[current_checkpoint], completion_amount);
        rb.MovePosition(new_position);
        if (completion_amount == 1)
        {
            current_checkpoint = (current_checkpoint + 1) % checkpoints_relative.Count;
            CalculateDistanceDifference();
            time_moving = 0;
        }
        time_moving += Time.fixedDeltaTime;
    }

    private void CalculateDistanceDifference()
    {
        distance_difference = (checkpoints_relative[GetPreviousCheckpoint()] - checkpoints_relative[current_checkpoint]).magnitude;
    }

    private float GetSpeedMultiplier()
    {
        if(current_checkpoint > checkpoints_speed_multiplier.Count - 1)
        {
            return 1;
        }
        return checkpoints_speed_multiplier[current_checkpoint];
    }

    private int GetPreviousCheckpoint() {
        int previous = current_checkpoint - 1;
        if(previous < 0)
        {
            previous = checkpoints_relative.Count - 1;
        }
        return previous;
    }
}
