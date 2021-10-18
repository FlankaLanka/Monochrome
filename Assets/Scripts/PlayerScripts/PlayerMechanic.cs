using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMechanic : MonoBehaviour
{
    GameObject gun;
    GameObject shield;
    Subscription<SwitchPlayerModeEvent> playerModeSubscription;

    void Awake()
    {
        playerModeSubscription = EventBus.Subscribe<SwitchPlayerModeEvent>(_OnModeSwitch);
        shield = transform.GetChild(0).gameObject;
        gun = transform.GetChild(1).gameObject;

        shield.SetActive(true);
        gun.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void _OnModeSwitch(SwitchPlayerModeEvent e)
    {
        if (e.mode == PlayerMode.Shield)
        {
            gun.SetActive(false);
            shield.SetActive(true);
        }
        else if (e.mode == PlayerMode.Shoot)
        {
            gun.SetActive(true);
            shield.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(playerModeSubscription);
    }
}
