using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public ProjectileManager p;

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown)
        {
            if (Input.GetKeyDown(e.keyCode))
            {
                // TODO: Charging the projectile.
                Debug.Log("Charging... (" + e.keyCode + ")");
            }
        }
        else if (e.type == EventType.KeyUp)
        {
            if (Input.GetKeyUp(e.keyCode))
            {
                p.CreateProjectile();
            }
        }
    }
}
