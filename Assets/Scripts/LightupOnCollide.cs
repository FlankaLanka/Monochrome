using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightupOnCollide : MonoBehaviour
{
    public Material lightUpMaterial;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            GetComponent<SpriteRenderer>().material = lightUpMaterial;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            GetComponent<SpriteRenderer>().material = lightUpMaterial;
        }
    }

}
