using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLevelOnHit : MonoBehaviour
{
    GameObject winDoor;
    Subscription<ProjectileColorChange> projColorChangeSubscription;


    private Animator anim;
    private SpriteRenderer projectileSprite;
    public bool ProjectileColorMatch;

    public string ColorProjectileKey;

    public AudioClip buttonNoise;

    private void Start()
    {
        if (System.String.IsNullOrWhiteSpace(ColorProjectileKey))
        {
            ProjectileColorMatch = true;
        }
        else
        {
            ProjectileColorMatch = false;
        }

        projColorChangeSubscription = EventBus.Subscribe<ProjectileColorChange>(_CorrectColorProj);
        winDoor = transform.GetChild(0).gameObject;
        anim = this.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        projectileSprite = collision.GetComponent<SpriteRenderer>();
        if(collision.gameObject.CompareTag("Projectile") && ProjectileColorMatch)
        {
            anim.SetTrigger("Hit");
            winDoor.SetActive(false);
            AudioSource.PlayClipAtPoint(buttonNoise, transform.position, 1);
            Destroy(collision.gameObject);
        }
    }

    private void _CorrectColorProj(ProjectileColorChange c)
    {
        ProjectileColorMatch = false;
        if (c.DidChange)
        {
            //ProjectileColorMatch = false;
            if (c.ChangedColor == ColorProjectileKey)
            {
                ProjectileColorMatch = true;
            }
            else
            {
                ProjectileColorMatch = false;
            }
        }
        /*else
        {
            ProjectileColorMatch = true;
        }*/
    }
}
