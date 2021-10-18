using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    public float rotation_multiplier = 15f;
    public float k = 0.3f;
    public float damp_factor = 0.85f;

    Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        Vector3 displacement = Vector3.zero - transform.localPosition;
        Vector3 a = k * displacement;

        velocity += a;
        velocity *= damp_factor;

        transform.localPosition += velocity;
    }

    public void Shake()
    {        
        transform.localPosition = UnityEngine.Random.onUnitSphere * 0.5f;
    }

    /*
    public void Shake(float duration = 0.2f, float power = 0.08f)
    {
        instance.StartCoroutine(ShakeCoroutine(duration, power));
    }

    IEnumerator ShakeCoroutine(float duration, float power)
    {
        Vector3 initialPosition = transform.position;

        float shake_fade_time = power / duration;
        float shake_rotation = power * rotation_multiplier;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float x = Random.Range(-1f, 1f) * power;
            float y = Random.Range(-1f, 1f) * power;

            transform.localPosition += new Vector3(x, y, 0f);
            transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(-1f, 1f) * shake_rotation);

            power = Mathf.MoveTowards(power, 0f, shake_fade_time * Time.deltaTime);
            shake_rotation = Mathf.MoveTowards(shake_rotation, 0f, shake_fade_time * Time.deltaTime);
            yield return null;
        }
        transform.localPosition = initialPosition;
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
    */
}
