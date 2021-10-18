using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePaintColorAndMechanic : MonoBehaviour
{
    public GameObject changeTo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile") && other.GetComponent<SpriteRenderer>().color != changeTo.GetComponent<SpriteRenderer>().color)
        {
            Vector3 projectilePosition = other.transform.position;
            Quaternion projectileRotation = other.transform.rotation;
            Vector3 projectileVelocity = other.GetComponent<Rigidbody2D>().velocity;
            
            // Destroy old projectile and create new one
            Destroy(other.gameObject);
            GameObject newProjectile = Instantiate<GameObject>(changeTo, projectilePosition, projectileRotation);
            newProjectile.GetComponent<Rigidbody2D>().velocity = projectileVelocity;
        }
    }
}
