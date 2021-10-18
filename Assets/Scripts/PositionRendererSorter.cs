using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRendererSorter : MonoBehaviour
{

    private Renderer rend;

    public int sortingOrderBase = 5000;
    public bool runOnce = true;

    // Start is called before the first frame update
    void Awake()
    {
        rend = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        rend.sortingOrder = (int)(sortingOrderBase - transform.position.y * 10);

        if(runOnce) {
            Destroy(this);
        }
    }
}
