using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTrigger : VisibleTrigger
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (trigger_flags.HasFlag(TriggerFlags.Player) && collision.gameObject.Equals(Player.GetPlayer().gameObject))
        {
            activated = !activated;
            trigger.Invoke();
            AudioSource audio = Player.GetPlayer().gameObject.GetComponent<AudioSource>();
            audio.PlayOneShot(Player.GetPlayer().toggle_audio);
            return;
        }
        if (trigger_flags.HasFlag(TriggerFlags.Projectile) && collision.gameObject.GetComponent<Projectile>() != null)
        {
            activated = !activated;
            trigger.Invoke();
            AudioSource audio = Player.GetPlayer().gameObject.GetComponent<AudioSource>();
            audio.PlayOneShot(Player.GetPlayer().toggle_audio);
            return;
        }
    }
}
