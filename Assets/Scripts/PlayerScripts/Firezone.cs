using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firezone : MonoBehaviour
{
    public GameObject projectile;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            EventBus.Publish<SwitchPlayerModeEvent>(new SwitchPlayerModeEvent(PlayerMode.Shoot));
            EventBus.Publish<SelectShootProjectile>(new SelectShootProjectile(projectile));
        }
            
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            EventBus.Publish<SwitchPlayerModeEvent>(new SwitchPlayerModeEvent(PlayerMode.Shield));
    }
}
