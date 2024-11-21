using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Platform : Moving
{
    private Vector2 movement = Vector2.zero;

    protected override void DoMovement()
    {
        float completion_amount = GetCompletionAmount();
        Vector2 new_position = Vector2.Lerp(checkpoints_relative[GetPreviousCheckpoint()], checkpoints_relative[current_checkpoint], completion_amount);
        movement = new_position - rb.position;
        MoveToPosition(new_position);
        if (completion_amount == 1)
        {
            current_checkpoint = (current_checkpoint + 1) % checkpoints_relative.Count;
            CalculateDistanceDifference();
            time_moving = 0;
        }
        time_moving += Time.fixedDeltaTime;
    }

    public Vector2 GetMovement()
    {
        return movement;
    }
}
