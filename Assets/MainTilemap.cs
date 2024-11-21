using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MainTilemap : MonoBehaviour
{
    private static Tilemap tilemap;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    public static Tilemap GetMainTilemap()
    {
        return tilemap;
    }
}
