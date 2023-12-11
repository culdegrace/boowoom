using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    Mesh projectile;
    Vector3 initialPosition;
    Vector3 direction;
    public float explosionForce = 750.0f;
    public float explosionRadius = 0.5f;

    void Init(Vector3 initialPosition, Vector3 direction)
    {
        this.initialPosition = initialPosition;
        gameObject.transform.position = initialPosition;
        this.direction = direction;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        // TODO: Explode!
        foreach (Rigidbody rb in ProjectileManager.allRigidbodies)
        {
            rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }

        Destroy(this.gameObject);

    }
}

public class ProjectileManager : MonoBehaviour
{

    public static List<Rigidbody> allRigidbodies = new List<Rigidbody>();

    public GameObject PROJECTILE; // From the .blend
    float magnitude = 3;
    float friction = 0.5f; // Dampen recoil.

    private Quaternion projectileRotation = Quaternion.identity;
    private Vector3 projectileDirection = Vector3.zero;
    private Vector3 projectilePosition = Vector3.zero;
    private float barrelLength = 0.5f;

    public GameObject parent; // For recoil and rocket jumping.
    Rigidbody parentRB;
    Quaternion parentDefaultRotation;
    float rotationSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // Default rotation values of parent. (prevents char from tipping).
        parentDefaultRotation = parent.transform.rotation;
        parentRB = parent.GetComponent<Rigidbody>();
        ProjectileManager.RegisterRigidbody(parentRB);
    }

    // If any projectile gets created, it will have these transforms.
    public void UpdateTransforms(Vector3 position, Quaternion rotation)
    {
        this.projectilePosition = position;
        this.projectileRotation = rotation;
        this.projectileDirection = rotation * Vector3.down;
    }

    public void CreateProjectile()
    {
        var offsetPos = this.projectilePosition + this.projectileDirection * this.barrelLength; // Offset by barrel length.
        GameObject projectileGO = (GameObject) Instantiate(PROJECTILE, offsetPos, this.projectileRotation);
        projectileGO.SetActive(true); // The platonic PROJECTILE begins unchecked.
        Projectile projectile = projectileGO.AddComponent<Projectile>();
        Rigidbody projectileRb = projectileGO.GetComponent<Rigidbody>();
        projectileRb.AddForce(magnitude * this.projectileDirection, ForceMode.Impulse);

        // Recoil.
        Rigidbody parentRB = parent.GetComponent<Rigidbody>();
        parentRB.AddForce(-magnitude * friction * this.projectileDirection, ForceMode.Impulse);

    }

    public static void RegisterRigidbody(Rigidbody rb)
    {
        if (!allRigidbodies.Contains(rb))
        {
            allRigidbodies.Add(rb);
        }
    }

    public static void UnregisterRigidbody(Rigidbody rb)
    {
        if (allRigidbodies.Contains(rb))
        {
            allRigidbodies.Remove(rb);
        }
    }

    void Update()
    {

    }
}
