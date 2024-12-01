using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public static Dictionary<int, Level> levels = new Dictionary<int, Level>();
    public int level_id;
    public string intro_name = "";
    public string intro_description = "";
    private Vector3 spawn_location_relative = new Vector3(0.5f, -0.5f, 0);
     
    private void Awake()
    {
        if(level_id == 0)
        {
            Debug.Log("Level id of 0: " + name);
        }
        if (levels.ContainsKey(level_id))
        {
            Debug.Log("Level id already in use: " + name + " and " + levels[level_id].name);
        }
        levels.Add(level_id, this);
        spawn_location_relative += transform.position;
    }

    public Vector3 GetSpawnLocation()
    {
        return spawn_location_relative;
    }

    public static Level GetCurrentLevel()
    {
        return levels[Player.GetPlayer().current_level];
    }
}
