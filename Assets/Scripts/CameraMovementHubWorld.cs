using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementHubWorld : MonoBehaviour
{
    public GameObject player;
    public float yOffset = .5f;
    public float xOffset = .5f;
    public Transform tutorialStop;

    public bool isZoomedOut = false;

    // Update is called once per frame
    void Update()
    {
        float yMin = -yOffset;
        float yMax = yOffset;
        float xMin = -xOffset;
        float xMax = xOffset;

        Transform pTransform = player.transform;

        if (isZoomedOut)
        {
            Vector3 zoomedOutPos = new Vector3(0, 23f, -10);
            transform.position = Vector3.Lerp(transform.position, zoomedOutPos, 2 * Time.deltaTime);
            //transform.position = new Vector3(0, 23f, -10);
        }
        else
        {

            if (pTransform.position.y < transform.position.y + yMin)
            {
                transform.position = new Vector3(transform.position.x, pTransform.position.y - yMin, -10);
            }
            else if (pTransform.position.y > transform.position.y + yMax)
            {
                transform.position = new Vector3(transform.position.x, pTransform.position.y - yMax, -10);
            }

            if (pTransform.position.x < transform.position.x + xMin)
            {
                transform.position = new Vector3(pTransform.position.x - xMin, transform.position.y, -10);
            }
            else if (pTransform.position.x > transform.position.x + xMax)
            {
                transform.position = new Vector3(pTransform.position.x - xMax, transform.position.y, -10);
            }

            // tutorial
            if (transform.position.x < tutorialStop.position.x)
            {
                transform.position = new Vector3(tutorialStop.position.x, transform.position.y, transform.position.z);
                if (transform.position.y < tutorialStop.position.y)
                {
                    transform.position = new Vector3(transform.position.x, tutorialStop.position.y, transform.position.z);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isZoomedOut = !isZoomedOut;
            if (isZoomedOut)
            {
                // Camera.main.orthographicSize = 15;
                StartCoroutine(zoomOutRoutine(.25f));
            }
            else
            {
                StartCoroutine(zoomNormalRoutine(.25f));
                //Camera.main.orthographicSize = 5;
            }
        }
        

    }

    IEnumerator zoomOutRoutine(float timeIn)
    {
        float elapsed = 0f;
        while (elapsed <= timeIn)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / timeIn);

            Camera.main.orthographicSize = Mathf.Lerp(5, 15, t);
            yield return null;
        }
    }

    IEnumerator zoomNormalRoutine(float timeIn)
    {
        float elapsed = 0;
        while (elapsed <= timeIn)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / timeIn);

            Camera.main.orthographicSize = Mathf.Lerp(15, 5, t);
            yield return null;
        }
    }
}
