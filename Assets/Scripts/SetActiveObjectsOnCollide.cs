using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveObjectsOnCollide : MonoBehaviour
{
    public List<GameObject> objects;
    public AudioClip startPowerNoise;
    public AudioClip startLightNoise;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            EventBus.Publish<TriggerPlayerCanMove>(new TriggerPlayerCanMove(false));
            StartCoroutine(SetObjectsActive());
        }
    }

    IEnumerator SetObjectsActive()
    {
        AudioSource.PlayClipAtPoint(startPowerNoise, transform.position);
        yield return new WaitForSeconds(1f);
        foreach (GameObject obj in objects)
        {
            obj.SetActive(true);
            AudioSource.PlayClipAtPoint(startLightNoise, transform.position);
            yield return new WaitForSeconds(.7f);
        }
        EventBus.Publish<TriggerPlayerCanMove>(new TriggerPlayerCanMove(true));
    }
}
