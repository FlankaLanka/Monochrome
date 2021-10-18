using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBouncePowerForBlue : MonoBehaviour
{
    public Sprite ShieldTopRight;
    public Sprite ShieldTopLeft;

    private Rigidbody2D rb;
    private Vector2 force;

    private void Start()
    {
        force = new Vector2(300, 300);
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("shield"))
        {
            //Debug.Log("WEWEWWWW");
            //Debug.Log(collision.gameObject.GetComponent<SpriteRenderer>().sprite.name);
            //Debug.Log(ShieldTopLeft.name);
            if(collision.gameObject.GetComponent<SpriteRenderer>().sprite == ShieldTopLeft)
            {
                //Debug.Log("ADADAD");
                rb.AddForce(force, ForceMode2D.Impulse);
            }
        }
    }
}
