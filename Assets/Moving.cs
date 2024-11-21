using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public List<Vector2> checkpoints_relative = new List<Vector2>();
    public List<float> checkpoints_speed_multiplier = new List<float>();
    protected Rigidbody2D rb;
    protected int current_checkpoint = 0;
    protected float time_moving = 0;
    protected float distance_difference = 0;

    private void Start()
    {
        if(checkpoints_relative.Count == 0)
        {
            return;
        }
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
        if (checkpoints_relative.Count == 0)
        {
            return;
        }
        DoMovement();
    }

    protected virtual void DoMovement()
    {
        float completion_amount = GetCompletionAmount();
        Vector2 new_position = Vector2.Lerp(checkpoints_relative[GetPreviousCheckpoint()], checkpoints_relative[current_checkpoint], completion_amount);
        MoveToPosition(new_position);
        if (completion_amount == 1)
        {
            current_checkpoint = (current_checkpoint + 1) % checkpoints_relative.Count;
            CalculateDistanceDifference();
            time_moving = 0;
        }
        time_moving += Time.fixedDeltaTime;
    }

    protected void MoveToPosition(Vector3 position)
    {
        if(GetSpeedMultiplier() == -1)
        {
            transform.position = position;
            return;
        }
        rb.MovePosition(position);
    }

    protected float GetCompletionAmount()
    {
        if (GetSpeedMultiplier() == -1)
        {
            return 1;
        }
        return Mathf.Min(1, (GetSpeedMultiplier() * time_moving) / distance_difference);
    }

    protected void CalculateDistanceDifference()
    {
        distance_difference = (checkpoints_relative[GetPreviousCheckpoint()] - checkpoints_relative[current_checkpoint]).magnitude;
    }

    protected float GetSpeedMultiplier()
    {
        if(current_checkpoint > checkpoints_speed_multiplier.Count - 1)
        {
            return 1;
        }
        return checkpoints_speed_multiplier[current_checkpoint];
    }

    protected int GetPreviousCheckpoint() {
        int previous = current_checkpoint - 1;
        if(previous < 0)
        {
            previous = checkpoints_relative.Count - 1;
        }
        return previous;
    }
}
