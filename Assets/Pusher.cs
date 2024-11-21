using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TriggerEvent;

public class Pusher : MonoBehaviour
{
    public Vector2 acceleration = new Vector2(0, 0);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Test");
        if (collision.gameObject.Equals(Player.GetPlayer().gameObject))
        {
            Player.AddVelocity(acceleration);
        }
    }
}
