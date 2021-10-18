using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMovement : MonoBehaviour
{

    private enum Direction {Top, Down, Left, Right, TopRight, TopLeft, DownRight, DownLeft};

    private Animator anim;

    public GameObject shield;
    public int bufferTime = 8;

    private Vector3 mousePos;
    private Vector2 mousePos2D;
    private Direction direction;
    private List<Direction> prevDirections;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        prevDirections = new List<Direction>();
    }

    // Update is called once per frame
    void Update()
    {
        // Set direction
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
            anim.SetTrigger("ShieldTop");
            shield.transform.localPosition = new Vector3(0, .07f, 0);
            shield.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if(direction == Direction.Right) {
            anim.SetTrigger("ShieldRight");
            shield.transform.localPosition = new Vector3(.165f, 0, 0);
            shield.transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if(direction == Direction.Left) {
            anim.SetTrigger("ShieldLeft");
            shield.transform.localPosition = new Vector3(-.16f, 0, 0);
            shield.transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if(direction == Direction.Down) {
            anim.SetTrigger("ShieldDown");
            shield.transform.localPosition = new Vector3(0, -.09f, 0);
            shield.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if(direction == Direction.TopRight) {
            anim.SetTrigger("ShieldTopRight");
            shield.transform.localPosition = new Vector3(.092f, 0.048f, 0);
            shield.transform.eulerAngles = new Vector3(0, 0, -45);
        }
        else if(direction == Direction.TopLeft) {
            anim.SetTrigger("ShieldTopLeft");
            shield.transform.localPosition = new Vector3(-.088f, 0.044f, 0);
            shield.transform.eulerAngles = new Vector3(0, 0, 45);
        }
        else if(direction == Direction.DownRight) {
            anim.SetTrigger("ShieldDownRight");
            shield.transform.localPosition = new Vector3(.1f, -.06f, 0);
            shield.transform.eulerAngles = new Vector3(0, 0, 45);
        }
        else {
            anim.SetTrigger("ShieldDownLeft");
            shield.transform.localPosition = new Vector3(-.1f, -.06f, 0);
            shield.transform.eulerAngles = new Vector3(0, 0, -45);
        }

        // mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // mousePos2D = new Vector2(mousePos.x, mousePos.y);

        // float angleToMouse = Mathf.Atan2(transform.position.y - mousePos.y, transform.position.x - mousePos.x) * 180 / Mathf.PI;

        // if(angleToMouse > 30 && angleToMouse <= 60) {
        //     anim.SetTrigger("ShieldDownLeft");

        //     shield.transform.localPosition = new Vector3(-.1f, -.06f, 0);
        //     shield.transform.eulerAngles = new Vector3(0, 0, -45);
        // }
        // // 90 degrees
        // else if(angleToMouse > 60 && angleToMouse <= 120) {
        //     anim.SetTrigger("ShieldDown");

        //     shield.transform.localPosition = new Vector3(0, -.08f, 0);
        //     shield.transform.eulerAngles = new Vector3(0, 0, 0);
        // }
        // // 135 degrees
        // else if(angleToMouse > 120 && angleToMouse <= 150) {
        //     anim.SetTrigger("ShieldDownRight");

        //     shield.transform.localPosition = new Vector3(.1f, -.06f, 0);
        //     shield.transform.eulerAngles = new Vector3(0, 0, 45);
        // }
        // // -45 degrees
        // else if(angleToMouse < -30 && angleToMouse >= -60) {
        //     anim.SetTrigger("ShieldTopLeft");

        //     shield.transform.localPosition = new Vector3(-.02f, 0, 0);
        //     shield.transform.eulerAngles = new Vector3(0, 0, 45);
        // }
        // // -90 degrees
        // else if(angleToMouse < -60 && angleToMouse >= -120) {
        //     anim.SetTrigger("ShieldTop");

        //     shield.transform.localPosition = new Vector3(0, 0, 0);
        //     shield.transform.eulerAngles = new Vector3(0, 0, 0);
        // }
        // // -135 degrees
        // else if(angleToMouse < -120 && angleToMouse >= -150) {
        //     anim.SetTrigger("ShieldTopRight");

        //     shield.transform.localPosition = new Vector3(.02f, 0, 0);
        //     shield.transform.eulerAngles = new Vector3(0, 0, -45);
        // }
        // // 180 degrees
        // else if(angleToMouse > 150 || angleToMouse < -150) {
        //     anim.SetTrigger("ShieldRight");

        //     shield.transform.localPosition = new Vector3(.165f, 0, 0);
        //     shield.transform.eulerAngles = new Vector3(0, 0, 90);
        // }
        // // 0 degrees
        // else {
        //     anim.SetTrigger("ShieldLeft");

        //     shield.transform.localPosition = new Vector3(-.065f, 0, 0);
        //     shield.transform.eulerAngles = new Vector3(0, 0, 90);
        // }
    }
}
