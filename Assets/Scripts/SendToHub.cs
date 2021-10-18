using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SendToHub : MonoBehaviour
{

    public PaintbrushColor color = PaintbrushColor.Orange;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player") {
            EventBus.Publish<PickupPaintbrush>(new PickupPaintbrush(color));
            SceneManager.LoadScene("HubWorld");
        }
    }
}
