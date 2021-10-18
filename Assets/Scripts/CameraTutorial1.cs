using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTutorial1 : MonoBehaviour
{
    public GameObject player;
    public float yOffset = 2;

    // Update is called once per frame
    void Update()
    {
        float yMin = -yOffset;
        float yMax = yOffset;
        Transform pTransform = player.transform;

        if(pTransform.position.y < transform.position.y + yMin) {
            transform.position = new Vector3(transform.position.x, pTransform.position.y - yMin, -10);
        }
        else if(pTransform.position.y > transform.position.y + yMax) {
            transform.position = new Vector3(transform.position.x, pTransform.position.y - yMax, -10);
        }
    }
}
