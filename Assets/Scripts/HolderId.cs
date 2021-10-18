using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolderId : MonoBehaviour
{
    public KeyDragAndDrop.Key key;

    private GameObject childKey;
    private SpriteRenderer rend;
    private bool empty = false;
    private bool prevEmpty = false;

    void Start()
    {
        childKey = transform.GetChild(0).gameObject;
        rend = this.GetComponent<SpriteRenderer>();
    }

    void Update() {
        if(childKey.transform.position != transform.position) {
            rend.color = Color.red;
            empty = true;
        }
        else
        {
            rend.color = Color.white;
            empty = false;
        }

        if(empty && !prevEmpty) {
            EventBus.Publish<UseKeyEvent>(new UseKeyEvent(key));
        }
        else if(!empty && prevEmpty) {
            EventBus.Publish<UseKeyEvent>(new UseKeyEvent(key));
        }
        prevEmpty = empty;
    }
}
