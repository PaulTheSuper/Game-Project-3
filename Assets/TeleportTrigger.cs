using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    public Vector2 teleport_position = new Vector2();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.Equals(Player.GetPlayer().gameObject))
        {
            Player.GetPlayer().gameObject.transform.position = teleport_position;
        }
    }

}
