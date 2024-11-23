using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    // Adds Everything to the list in the inspector
    [System.Flags]
    public enum TriggerFlags
    {
        Player = 1,
        Projectile = 2,
    }

    public UnityEvent trigger;
    public TriggerFlags trigger_flags;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(trigger_flags.HasFlag(TriggerFlags.Player) && collision.gameObject.Equals(Player.GetPlayer().gameObject))
        {
            trigger.Invoke();
            return;
        }
        if(trigger_flags.HasFlag(TriggerFlags.Projectile) && collision.gameObject.GetComponent<Projectile>() != null)
        {
            trigger.Invoke();
            return;
        }
    }
    
    public void SpawnPrefab(GameObject prefab)
    {
        GameObject spawned = Instantiate(prefab);
        spawned.transform.position += transform.position;
    }

    public void ToggleActive(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }

    public void SetActive(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void SetInactive(GameObject obj)
    {
        obj.SetActive(false);
    }
}
