using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActive : MonoBehaviour
{
    public List<float> toggle_time = new List<float>();
    public List<bool> toggle_state = new List<bool>();
    private float current_time_left = 0;
    private int current_index = 0;
    private bool active_state = true;

    private void Start()
    {
        current_time_left = toggle_time[current_index];
        UpdateState();
    }

    private void FixedUpdate()
    {
        current_time_left -= Time.fixedDeltaTime;
        if (current_time_left < 0)
        {
            current_index = (current_index + 1) % toggle_time.Count;
            UpdateState();
            // Add whatever is left in the current time left that is now negative to make stuff not desync as hard?
            current_time_left = toggle_time[current_index] + current_time_left;
        }
    }

    private void UpdateState()
    {
        // Check if we are actually swapping states and can move the object back by the same offset
        if (toggle_state[current_index] && active_state == false)
        {
            transform.position -= new Vector3(0, 10000, 0);
            active_state = true;
        }
        else if (!toggle_state[current_index] && active_state == true)
        {
            transform.position += new Vector3(0, 10000, 0);
            active_state = false;
        }
    }
}
