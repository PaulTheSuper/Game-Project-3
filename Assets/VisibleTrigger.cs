using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleTrigger : TriggerEvent
{
    public bool toggle = false;
    protected bool activated = false;
    protected SpriteRenderer sprite_renderer;

    private void Start()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (sprite_renderer == null)
        {
            return;
        }
        if (activated)
        {
            sprite_renderer.color = Color.blue;
        }
        else
        {
            sprite_renderer.color = new Color(50.0f / 255, 50.0f / 255, 50.0f / 255);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activated)
        {
            return;
        }
        if (trigger_flags.HasFlag(TriggerFlags.Player) && collision.gameObject.Equals(Player.GetPlayer().gameObject))
        {
            activated = true;
            trigger.Invoke();
            AudioSource audio = Player.GetPlayer().gameObject.GetComponent<AudioSource>();
            audio.PlayOneShot(Player.GetPlayer().toggle_audio);
            return;
        }
        if (trigger_flags.HasFlag(TriggerFlags.Projectile) && collision.gameObject.GetComponent<Projectile>() != null)
        {
            activated = true;
            trigger.Invoke();
            AudioSource audio = Player.GetPlayer().gameObject.GetComponent<AudioSource>();
            audio.PlayOneShot(Player.GetPlayer().toggle_audio);
            return;
        }
    }

    public void ToggleVisibleTrigger()
    {
        activated = !activated;
    }

    public bool IsActivated()
    {
        return activated;
    }

    public void DeactivateAfterSeconds(float time)
    {
        if (!activated)
        {
            return;
        }
        StartCoroutine(DeactivateCoroutine(time)); ;
    }

    IEnumerator DeactivateCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        activated = false;
        if (toggle)
        {
            trigger.Invoke();
        }
    }
}
