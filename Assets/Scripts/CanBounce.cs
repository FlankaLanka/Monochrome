using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanBounce : MonoBehaviour
{
    public float speed = 10f;
    public bool didchangeproj;
    public AudioClip bounceSound;

    public Sprite ShieldTopRight;
    public Sprite ShieldTopLeft;

    private Animator anim;
    

    Rigidbody2D rb;
    Vector2 lastVelocity;
    //private CanChangePaint paintchange;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        didchangeproj = false;
    }

    private void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<isBouncy>() != null)// && gameObject.tag == collision.gameObject.tag)
        {
            AudioSource.PlayClipAtPoint(bounceSound, transform.position);
            Vector2 newDirection = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal).normalized;
            rb.velocity = newDirection * speed;
            rb.rotation = collision.gameObject.GetComponent<Rigidbody2D>().rotation;
            anim.SetTrigger("PaintballBounce");
            
            if (GetComponent<SpriteRenderer>().color == new Color(0f,0.4f,1f,1f))
            {
                if(collision.gameObject.transform.parent.GetComponent<SpriteRenderer>().sprite == ShieldTopRight)
                {
                    rb.AddForce(new Vector2(-1f,5f), ForceMode2D.Impulse);
                }
                else if(collision.gameObject.transform.parent.GetComponent<SpriteRenderer>().sprite == ShieldTopLeft)
                {
                    rb.AddForce(new Vector2(-1f, 5f), ForceMode2D.Impulse);
                }
            }
        }
        else
        {
            if (didchangeproj)
            {
                EventBus.Publish<ProjectileColorChange>(new ProjectileColorChange("", true));
            }
            Destroy(gameObject);
        }
    }
    
}
