using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    BoxCollider collider = null;
    Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);

    private void Start()
    {
        // Create a Box Collider that matches the combined bounds
        this.collider = gameObject.AddComponent<BoxCollider>();

    }
    void Update()
    {
        // Calculate combined bounds of all child meshes in local space
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            this.bounds.Encapsulate(transform.InverseTransformPoint(renderer.bounds.min));
            this.bounds.Encapsulate(transform.InverseTransformPoint(renderer.bounds.max));
        }

        // Transform the combined bounds back to world space
        this.bounds.center = transform.TransformPoint(this.bounds.center);

        this.collider.center = this.bounds.center - transform.position;
        this.collider.size = this.bounds.size;
    }
}