using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aimer : MonoBehaviour
{
    Transform baseTransform;
    Transform hingeTransform;
    Transform barrelTransform;
    // Start is called before the first frame update
    void Start()
    {
        baseTransform = transform.Find("BASE");
        hingeTransform = transform.Find("HINGE");
        barrelTransform = transform.Find("BARREL");
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(hit.transform.name == "STAGE")
            {
                // Mouse intersects with stage.
                var lookAtPosition = hit.point;
                // Swiveling the hinge.
                var hingeOffs = Quaternion.LookRotation(lookAtPosition - hingeTransform.position, Vector3.up);
                var hingeDefault = hingeTransform.eulerAngles;
                var hingeRotation = Quaternion.Euler(hingeDefault.x, hingeOffs.eulerAngles.y, hingeDefault.z);
                var hingeLerp = Quaternion.Lerp(hingeTransform.rotation, hingeRotation, 0.1f);
                hingeTransform.rotation = hingeLerp;

                // Rotating the barrel.
                var barrelOffs = Quaternion.LookRotation(lookAtPosition - barrelTransform.position, Vector3.up);
                var barrelDefault = barrelTransform.eulerAngles;
                var barrelRotation = Quaternion.Euler(barrelOffs.eulerAngles.x - 90, barrelOffs.eulerAngles.y, barrelOffs.eulerAngles.z);
                var barrelLerp = Quaternion.Lerp(barrelTransform.rotation, barrelRotation, 0.1f);
                barrelTransform.rotation = barrelLerp;
                

            }
        }
    }
}
