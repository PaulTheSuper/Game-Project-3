using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActive : MonoBehaviour
{
    public float seconds_active = 1;
    public float seconds_inactive = 1;
    private float time_passed = 0;
    private bool active_state = true;

    private void FixedUpdate()
    {
        time_passed += Time.fixedDeltaTime;
        if(active_state && time_passed >= seconds_active)
        {
            active_state = false;
            transform.position += new Vector3(0, 10000, 0);
            time_passed = 0;
        }
        if(!active_state && time_passed >= seconds_inactive)
        {
            active_state = true;
            transform.position -= new Vector3(0, 10000, 0);
            time_passed = 0;
        }
    }
}
