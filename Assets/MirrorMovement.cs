using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector3 center_around = new Vector3(0, 0, 0);


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector3 difference_to_player = Player.GetPlayer().transform.position - center_around;
        rb.MovePosition(center_around - difference_to_player);
    }
}
