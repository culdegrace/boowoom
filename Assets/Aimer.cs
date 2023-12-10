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
        hingeTransform = baseTransform.Find("HINGE");
        barrelTransform = hingeTransform.Find("BARREL");
    }

    // Update is called once per frame
    void Update()
    {
        float rotationSpeed = 45.0f;
        float step = rotationSpeed * Time.deltaTime;
        baseTransform.Rotate(0, 0, step);
    }
}
