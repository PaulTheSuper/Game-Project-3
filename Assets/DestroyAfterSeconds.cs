using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    public float destroy_time = 5;

    private void Start()
    {
        Destroy(gameObject, destroy_time);
    }
}
