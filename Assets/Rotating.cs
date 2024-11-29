using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    public float rotation_per_second = 90;
    private Vector3 start_of_tick_angles = new Vector3(0, 0, 0);
    private float time_passed = 0;

    // Interpolate rotation
    private void Update()
    {
        time_passed += Time.deltaTime;
        transform.eulerAngles = start_of_tick_angles + new Vector3(0, 0, rotation_per_second * Time.fixedDeltaTime * time_passed / Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        time_passed = 0;
        start_of_tick_angles = transform.eulerAngles;
        transform.eulerAngles = transform.eulerAngles + new Vector3(0, 0, rotation_per_second * Time.fixedDeltaTime);
    }
}
