using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnZeroVelocity : MonoBehaviour
{
    public AudioClip destroyNoise;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity == Vector2.zero)
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(destroyNoise, Camera.main.transform.position, 0.5f);
            CameraShake.instance.Shake();
            ParticleSystemManager.RequestParticlesAtPositionAndDirection(rb.transform.position, Vector3.zero, GetComponent<SpriteRenderer>().color);
        }
    }
}
