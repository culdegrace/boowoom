using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aimer : MonoBehaviour
{
    public ProjectileManager p;

    public Transform baseTransform;
    public Transform hingeTransform;
    public Transform barrelTransform;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    // ROTATE TWD MOUSE
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Intersect either with the stage OR with this infinite plane
        var groundPlane = new Plane(Vector3.up, -0.1f);
        float enter = 0.0f;

        bool found = false;
        var lookAtPosition = Vector3.zero;
        // Mouse intersects with stage.
        if (Physics.Raycast(ray, out hit))
        {
            if(hit.transform.name == "STAGE")
            {
                found = true;
                lookAtPosition = hit.point;
            }
        }
        // Mouse intersects with area outside of the stage (groundPlane above)
        else if (groundPlane.Raycast(ray, out enter))
        {
            found = true;
            lookAtPosition = ray.GetPoint(enter);
        }

        if (!found)
        {
            return;
        }

        // Swiveling the hinge.
        var hingeOffs = Quaternion.LookRotation(lookAtPosition - hingeTransform.position, Vector3.up);
        var hingeDefault = hingeTransform.eulerAngles;
        var hingeRotation = Quaternion.Euler(hingeDefault.x, hingeOffs.eulerAngles.y, hingeDefault.z); // Rotate y axis only.
        var hingeLerp = Quaternion.Lerp(hingeTransform.rotation, hingeRotation, 0.1f);
        hingeTransform.rotation = hingeLerp;

        // Rotating the barrel.
        var barrelOffs = Quaternion.LookRotation(lookAtPosition - barrelTransform.position, Vector3.up);
        var barrelRotation = Quaternion.Euler(barrelOffs.eulerAngles.x - 90, barrelOffs.eulerAngles.y, barrelOffs.eulerAngles.z);
        var barrelLerp = Quaternion.Lerp(barrelTransform.rotation, barrelRotation, 0.1f);
        barrelTransform.rotation = barrelLerp;

    // POLL INPUT
        if (Input.anyKey)
        {
            Debug.Log("Drawing ray.");
            // Direction of barrel.
            Quaternion rot = barrelLerp;
            Vector3 directionToFace = barrelLerp * Vector3.down;
            Debug.DrawRay(barrelTransform.position, directionToFace, Color.red, 2f);
            p.CreateProjectile(barrelTransform.position + .5f * directionToFace, directionToFace, rot);
        }
    }
}
