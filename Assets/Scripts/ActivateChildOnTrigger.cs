using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateChildOnTrigger : MonoBehaviour
{
    private GameObject message;

    void Start()
    {
        message = transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            message.SetActive(true);
            StartCoroutine(FadeTo(1.0f, .25f));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FadeTo(0.0f, .25f));
            StartCoroutine(Deactivate(.25f));
        }
    }

    IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = message.GetComponent<SpriteRenderer>().color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,aValue,t));
            message.GetComponent<SpriteRenderer>().color = newColor;
            yield return null;
        }
    }

    IEnumerator Deactivate(float aTime)
    {
        yield return new WaitForSeconds(aTime);
        message.SetActive(false);
    }
}
