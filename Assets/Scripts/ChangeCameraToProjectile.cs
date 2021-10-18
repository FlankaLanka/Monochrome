using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraToProjectile : MonoBehaviour
{
    public GameObject cameraContainer;
    public Vector3 newPosition;
    
    private Vector3 originalPosition;
    private GameObject targetProjectile;
    float cameraMoveDuration = 1f;
    bool triggered = false;


    // Start is called before the first frame update
    void Start()
    {
        originalPosition = cameraContainer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetProjectile == null && triggered)
        {
            StartCoroutine(MoveCamera(originalPosition, 1.5f));
            triggered = false;
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            targetProjectile = collision.gameObject;
            StartCoroutine(MoveCamera(newPosition));
            triggered = true;
        }
    }

    IEnumerator MoveCamera(Vector3 destination, float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        cameraContainer.transform.position = destination;
    }
}
