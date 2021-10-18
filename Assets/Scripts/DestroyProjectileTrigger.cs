using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectileTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(other.gameObject); // this is to destory when hitting back of shield (has a trigger col on back of shield)
        }
    }
    
}
