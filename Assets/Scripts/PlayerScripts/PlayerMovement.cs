using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public enum Direction {
        Horizontal, Vertical, TiltRight, TiltLeft
    }

    private Direction direction = Direction.Horizontal;
    public float speed = 8.0f;
    public float dashSpeed = 40f;
    public float speedDecreasePercent = .25f;

    private Rigidbody2D rb;
    private Animator anim;

    private bool canMove = true;
    //private bool isDashing = false;
    private bool moving = false;
    private bool isSlow = false;

    Subscription<SpawnEndEvent> spawnEndSubscription;
    Subscription<TriggerPlayerCanMove> playerCanMoveSubscription;

    AudioSource[] sounds;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sounds = GetComponents<AudioSource>();

        spawnEndSubscription = EventBus.Subscribe<SpawnEndEvent>(_OnSpawnEnd);
        playerCanMoveSubscription = EventBus.Subscribe<TriggerPlayerCanMove>(_OnPlayerCanMoveTrigger);
    }

    private void Update()
    {
        if(canMove) {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            float xRaw = Input.GetAxisRaw("Horizontal");
            float yRaw = Input.GetAxisRaw("Vertical");

            if(x > 0) {
                direction = Direction.Horizontal;
                if(y < 0) {
                    direction = Direction.TiltLeft;
                }
                else if(y > 0) {
                    direction = Direction.TiltRight;
                }
                moving = true;
            }
            if(x < 0) {
                direction = Direction.Horizontal;
                if(y < 0) {
                    direction = Direction.TiltRight;
                }
                else if(y > 0) {
                    direction = Direction.TiltLeft;
                }
                moving = true;
            }
            if((y > 0 || y < 0) && x == 0) {
                direction = Direction.Vertical;
                moving = true;
            }
            
            if(x != 0 && y != 0) {
                x = new Vector3(x, y).normalized.x;
                y = new Vector3(x, y).normalized.y;
            }

            Animate();

            Walk(new Vector2(x, y));

            /*if (Input.GetKeyDown(KeyCode.Space) && !isDashing && (xRaw != 0 || yRaw != 0))
                Dash(new Vector2(xRaw, yRaw));*/

            moving = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //Debug.Log("isSlow is true");
            isSlow = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            //Debug.Log("isSlow is false");
            isSlow = false;
        }
    }

    private void Animate()
    {
        if(direction == Direction.Horizontal) {
            if(!moving) {
                anim.SetTrigger("IdleHorizontal");
            }
            else {
                anim.SetTrigger("MoveHorizontal");
            }
        }
        else if(direction == Direction.Vertical) {
            if(!moving) {
                anim.SetTrigger("IdleVertical");
            }
            else {
                anim.SetTrigger("MoveVertical");
            }
        }
        else if(direction == Direction.TiltRight) {
            if(!moving) {
                anim.SetTrigger("IdleTiltRight");
            }
            else {
                anim.SetTrigger("MoveTiltRight");
            }
        }
        else {
            if(!moving) {
                anim.SetTrigger("IdleTiltLeft");
            }
            else {
                anim.SetTrigger("MoveTiltLeft");
            }
        }
    }

    private void Walk(Vector2 direction)
    {
        /*if (isDashing)
            return;*/
        if (isSlow)
        {
            rb.velocity = direction * speed * speedDecreasePercent;

            sounds[1].Stop();
            if (rb.velocity != Vector2.zero)
            {
                if (!sounds[0].isPlaying)
                    sounds[0].Play();
            }                
            else
                sounds[0].Stop();
        }
        else
        {
            rb.velocity = direction * speed;

            sounds[0].Stop();
            if (rb.velocity != Vector2.zero)
            {
                if (!sounds[1].isPlaying)
                    sounds[1].Play();
            }
            else
                sounds[1].Stop();
        }
    }

    /*private void Dash(Vector2 direction)
    {    
        rb.velocity = direction.normalized * dashSpeed;
        StartCoroutine(DashCoroutine(0.25f));
    }*/

    /*IEnumerator DashCoroutine(float duration)
    {
        isDashing = true;
        rb.drag = 14f;
        float elapsed = 0;
        while (elapsed < duration)
        {
            rb.drag = Mathf.Lerp(14, 0, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        rb.drag = 0;
        rb.velocity = Vector2.zero;
        isDashing = false;
    }*/

    void onTriggerEnter2D(Collider2D col) {
        print("HERE");
        if(col.gameObject.tag == "Next Room Trigger") {
            print("OK!");
            
        }
    }

    private void _OnSpawnEnd(SpawnEndEvent e) {

    }

    private void _OnPlayerCanMoveTrigger(TriggerPlayerCanMove e)
    {
        canMove = e.playerCanMove;

        if (!canMove)
            rb.velocity = Vector2.zero;
    }
}
