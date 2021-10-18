using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMovement : MonoBehaviour
{

    private enum Direction {Top, Down, Left, Right, TopRight, TopLeft, DownRight, DownLeft};

    private Animator anim;

    public GameObject ProjectileObj;
    public AudioClip shootNoise;
    public float bulletSpeed = 10;
    public int bufferTime = 8;
    public float shootCooldownDuration = 0.7f;
    public float playerDelayAfterShot = .1f;

    private Vector3 mousePos;
    private Vector2 mousePos2D;
    private bool shotFired = false;
    private Direction direction;
    private List<Direction> prevDirections;
    private float lastShootTime = 0;

    Subscription<SelectShootProjectile> selectProjectileSubscription;


    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        prevDirections = new List<Direction>();
        selectProjectileSubscription = EventBus.Subscribe<SelectShootProjectile>(_OnProjectileSelection);

    }

    // Update is called once per frame
    void Update()
    {
        // Get Inputs
        if(Input.GetKey(KeyCode.UpArrow)) {
            direction = Direction.Top;
        }
        if(Input.GetKey(KeyCode.DownArrow)) {
            direction = Direction.Down;
        }
        if(Input.GetKey(KeyCode.LeftArrow)) {
            direction = Direction.Left;
            if(Input.GetKey(KeyCode.UpArrow)) {
                direction = Direction.TopLeft;
            }
            else if(Input.GetKey(KeyCode.DownArrow)) {
                direction = Direction.DownLeft;
            }
        }
        if(Input.GetKey(KeyCode.RightArrow)) {
            direction = Direction.Right;
            if(Input.GetKey(KeyCode.UpArrow)) {
                direction = Direction.TopRight;
            }
            else if(Input.GetKey(KeyCode.DownArrow)) {
                direction = Direction.DownRight;
            }
        }

        if(Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.DownArrow)) {
            for(int i = 0; i < bufferTime; ++i) {
                if(prevDirections[i] == Direction.TopRight ||
                   prevDirections[i] == Direction.TopLeft ||
                   prevDirections[i] == Direction.DownRight ||
                   prevDirections[i] == Direction.DownLeft) {
                    direction = prevDirections[i];
                }
            }
        }

        prevDirections.Insert(0, direction);
        if(prevDirections.Count > bufferTime) {
            prevDirections.RemoveAt(bufferTime);
        }

        // Set animation
        if(direction == Direction.Top) {
            anim.SetTrigger("GunTop");
            if(shotFired) {
                anim.SetTrigger("GunShootTop");
            }
        }
        else if(direction == Direction.Right) {
            anim.SetTrigger("GunRight");
            if(shotFired) {
                anim.SetTrigger("GunShootRight");
            }
        }
        else if(direction == Direction.Left) {
            anim.SetTrigger("GunLeft");
            if(shotFired) {
                anim.SetTrigger("GunShootLeft");
            }
        }
        else if(direction == Direction.Down) {
            anim.SetTrigger("GunDown");
            if(shotFired) {
                anim.SetTrigger("GunShootDown");
            }
        }
        else if(direction == Direction.TopRight) {
            anim.SetTrigger("GunTopRight");
            if(shotFired) {
                anim.SetTrigger("GunShootTopRight");
            }
        }
        else if(direction == Direction.TopLeft) {
            anim.SetTrigger("GunTopLeft");
            if(shotFired) {
                anim.SetTrigger("GunShootTopLeft");
            }
        }
        else if(direction == Direction.DownRight) {
            anim.SetTrigger("GunDownRight");
            if(shotFired) {
                anim.SetTrigger("GunShootDownRight");
            }
        }
        else {
            anim.SetTrigger("GunDownLeft");
            if(shotFired) {
                anim.SetTrigger("GunShootDownLeft");
            }
        }

        shotFired = false;

        // Player attempts to shoot
        if(Input.GetKeyUp(KeyCode.Space) && (Time.time - lastShootTime >= shootCooldownDuration)) {
            StartCoroutine(Shoot());
            shotFired = true;
            lastShootTime = Time.time;
        }
    }

    IEnumerator Shoot() 
    {
        EventBus.Publish<TriggerPlayerCanMove>(new TriggerPlayerCanMove(false));
        Vector2 shootDirection;
        if(direction == Direction.Top) {
            shootDirection = Vector2.up;
        }
        else if(direction == Direction.Right) {
            shootDirection = Vector2.right;
        }
        else if(direction == Direction.Left) {
            shootDirection = Vector2.left;
        }
        else if(direction == Direction.Down) {
            shootDirection = Vector2.down;
        }
        else if(direction == Direction.TopRight) {
            shootDirection = new Vector2(1,1);
        }
        else if(direction == Direction.TopLeft) {
            shootDirection = new Vector2(-1,1);
        }
        else if(direction == Direction.DownRight) {
            shootDirection = new Vector2(1,-1);
        }
        else {
            shootDirection = new Vector2(-1,-1);
        }
        shootDirection.Normalize();

        yield return new WaitForSeconds(.4f);

        AudioSource.PlayClipAtPoint(shootNoise, transform.position, 1);
        GameObject projectile = Instantiate(ProjectileObj, new Vector3(transform.position.x, transform.position.y + .2f, transform.position.z), Quaternion.identity);

        projectile.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletSpeed;

        // TODO: Add CameraShake script to main camera for this to work
        CameraShake.instance.Shake();
        
        //SpawnLimit--;

        // Delay before player can move again
        yield return new WaitForSeconds(playerDelayAfterShot);
        EventBus.Publish<TriggerPlayerCanMove>(new TriggerPlayerCanMove(true));
    }

    private void _OnProjectileSelection(SelectShootProjectile e)
    {
        ProjectileObj = e.projectile;
    }
}
