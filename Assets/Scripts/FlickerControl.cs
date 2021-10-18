using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerControl : MonoBehaviour
{
    public float minDelay;
    public float maxDelay;

    float timeDelay;
    bool isFlickering = false;

    // Update is called once per frame
    void Update()
    {
        if (!isFlickering)
            StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        isFlickering = true;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        timeDelay = Random.Range(minDelay, maxDelay);
        yield return new WaitForSeconds(timeDelay);

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        timeDelay = Random.Range(minDelay, maxDelay);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }
}
