using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Parallax : MonoBehaviour
{
    public float scroll_speed = 0.5f;

    private void Update()
    {
        transform.position = Camera.main.transform.position * scroll_speed;
    }
}
