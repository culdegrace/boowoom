using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Mesh projectile;
    Vector3 initialPosition;
    Vector3 direction;
    void Init(Vector3 initialPosition, Vector3 direction)
    {
        this.initialPosition = initialPosition;
        gameObject.transform.position = initialPosition;
        this.direction = direction;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}

public class ProjectileManager : MonoBehaviour
{
    public GameObject PROJECTILE; // From the .blend
    float magnitude = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CreateProjectile(Vector3 location, Vector3 direction, Quaternion rotation)
    {
        GameObject projectileGO = (GameObject) Instantiate(PROJECTILE, location, rotation);
        projectileGO.SetActive(true); // The platonic PROJECTILE begins unchecked.
        Projectile projectile = projectileGO.AddComponent<Projectile>();
        Rigidbody projectileRb = projectileGO.GetComponent<Rigidbody>();
        projectileRb.AddForce(magnitude * direction, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
