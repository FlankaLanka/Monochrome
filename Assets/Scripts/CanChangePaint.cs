using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanChangePaint : MonoBehaviour
{
    SpriteRenderer projectileSprite;
    public string ChangeToColor;
    public bool didChangeAProj;


    private CanBounce colorChangeInstance;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            colorChangeInstance = other.GetComponent<CanBounce>();
            colorChangeInstance.didchangeproj = true;

            projectileSprite = other.GetComponent<SpriteRenderer>();
            if (ChangeToColor == "red")
            {
                projectileSprite.color = Color.red;
                EventBus.Publish<ProjectileColorChange>(new ProjectileColorChange("red", true));
            }
            else if (ChangeToColor == "blue")
            {
                projectileSprite.color = Color.magenta;
                EventBus.Publish<ProjectileColorChange>(new ProjectileColorChange("blue", true));
            }
        }
                
    }
}
