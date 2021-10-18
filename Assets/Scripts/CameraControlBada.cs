using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlBada : MonoBehaviour
{
    public GameObject player;
 

    public float LevelMinX;
    public float LevelMaxX;
    public float LevelMinY;
    public float LevelMaxY;

    private Transform playerTransform;

    void Start()
    {
        playerTransform = player.transform;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 playerPos = playerTransform.position;
        playerPos.z = transform.position.z;
        transform.position = playerPos;

        transform.position = new Vector3
       (
           Mathf.Clamp(transform.position.x, LevelMinX, LevelMaxX),
           Mathf.Clamp(transform.position.y, LevelMinY, LevelMaxY),
           transform.position.z
       );


        /*float yMin = -yOffset;
        float yMax = yOffset;
        float xMin = -xOffset;
        float xMax = xOffset;
        
        Transform pTransform = player.transform;

        if (pTransform.position.y < transform.position.y + yMin)
        {
            transform.position = new Vector3(transform.position.x, pTransform.position.y - yMin, -10);
        }
        else if (pTransform.position.y > transform.position.y + yMax)
        {
            transform.position = new Vector3(transform.position.x, pTransform.position.y - yMax, -10);
        }
        else if (pTransform.position.x < transform.position.x + xMin)
        {
            transform.position = new Vector3(pTransform.position.x - xMin, transform.position.y, -10);
        }
        else if (pTransform.position.x > transform.position.x + xMax)
        {
            transform.position = new Vector3(pTransform.position.x - xMax, transform.position.y, -10);
        }
        else
        {
            transform.position = new Vector3(pTransform.position.x, pTransform.position.y, -10);
        }*/
    }
}
