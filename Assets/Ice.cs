using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : Ground
{
    private void Start()
    {
        speed = 1.0f;
        friction_modifier = 0.95f;
    }
}
