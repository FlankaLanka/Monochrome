using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMove : MonoBehaviour
{
    public GameObject player;
    public Transform newPlayerPosition;
    public Camera newCamera;

    float playerMoveDuration = 1.5f;
    float cameraMoveDuration = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EventBus.Publish<TriggerPlayerCanMove>(new TriggerPlayerCanMove(false));
            StartCoroutine(MoveRoom());
        }
    }

    private IEnumerator MoveRoom()
    {
        // Move Camera
        float startTime = Time.time;
        float progress = 0;
        Vector3 cameraStartPosition = Camera.main.transform.parent.transform.position;
        float cameraStartOrthSize = Camera.main.orthographicSize;

        while (progress < 1.0f)
        {
            progress = (Time.time - startTime) / cameraMoveDuration;
            Camera.main.transform.parent.transform.position = Vector3.Lerp(cameraStartPosition, newCamera.transform.position, progress);
            Camera.main.orthographicSize = Mathf.Lerp(cameraStartOrthSize, newCamera.orthographicSize, progress);
            yield return null;
        }

        Camera.main.transform.parent.transform.position = newCamera.transform.position;
        Camera.main.orthographicSize = newCamera.orthographicSize;

        // Move Player after moving camera
        StartCoroutine(MovePlayer());
    }

    private IEnumerator MovePlayer()
    {
        float startTime = Time.time;
        float progress = 0;
        Vector3 startPosition = player.transform.position;

        while (progress < 1.0f)
        {
            progress = (Time.time - startTime) / playerMoveDuration;
            player.transform.position = Vector3.Lerp(startPosition, newPlayerPosition.position, progress);
            yield return null;
        }

        player.transform.position = newPlayerPosition.position;
        EventBus.Publish<TriggerPlayerCanMove>(new TriggerPlayerCanMove(true));
    }
}
