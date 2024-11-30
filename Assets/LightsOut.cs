using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightsOut : MonoBehaviour
{
    public List<VisibleTrigger> triggers = new List<VisibleTrigger>();
    public UnityEvent on_completion;
    private bool has_ran = false;

    private void FixedUpdate()
    {
        if(has_ran)
        {
            return;
        }
        for (int i = 0; i < triggers.Count; i++)
        {
            VisibleTrigger trigger = triggers[i];
            if(!trigger.IsActivated())
            {
                return;
            }
        }
        on_completion.Invoke();
        has_ran = true;
    }
}
