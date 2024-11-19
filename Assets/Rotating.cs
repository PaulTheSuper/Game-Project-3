using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    public float rotation_per_second = 90;

    private void FixedUpdate()
    {
        transform.eulerAngles = transform.eulerAngles + new Vector3(0, 0, rotation_per_second * Time.fixedDeltaTime);
    }
}
