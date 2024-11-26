using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    protected float speed = 10;
    protected float friction_modifier = 0;

    public float GetSpeed()
    {
        return speed;
    }

    public float GetFrictionModifier ()
    {
        return friction_modifier;
    }
}
