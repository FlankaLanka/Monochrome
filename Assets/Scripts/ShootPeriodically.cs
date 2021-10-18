using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPeriodically : MonoBehaviour
{

    public GameObject ProjectileObj;
    public float period = 1.5f;
    public float bulletSpeed = 10;
    public Vector2 shootDirection;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot() 
    {
        for(;;) {
            //Vector2 shootDirection = Vector2.down;
            anim.SetTrigger("Shoot");

            yield return new WaitForSeconds(.4f);

            //AudioSource.PlayClipAtPoint(shootNoise, transform.position, 1);
            GameObject projectile = Instantiate(ProjectileObj, new Vector3(transform.position.x, transform.position.y + .2f, transform.position.z), Quaternion.identity);

            projectile.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletSpeed;

            // TODO: Add CameraShake script to main camera for this to work
            //CameraShake.instance.Shake();
            
            //SpawnLimit--;
            
            yield return new WaitForSeconds(period);
        }
        
    }
}
