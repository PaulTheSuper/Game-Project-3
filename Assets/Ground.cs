using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ground : MonoBehaviour
{
    protected float speed;
    protected float friction_modifier;

    public float GetSpeed()
    {
        return speed;
    }

    public float GetFrictionModifier ()
    {
        return friction_modifier;
    }
}
