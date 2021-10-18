using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDragAndDrop : MonoBehaviour
{

    private SpriteRenderer rend;
    private Collider col;

    public enum Key {Right, Left, Top, Down, Space};
    public Key key;

    private bool dragging = false;
    private bool rotatable = false;
    private bool rotating = false;
    private Vector3 mousePos;
    private Vector2 mousePos2D;

    // Start is called before the first frame update
    void Start()
    {
        rend = this.GetComponent<SpriteRenderer>();
        col = this.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos2D = new Vector2(mousePos.x, mousePos.y);

        // Player clicks on key to drag
        if(Input.GetMouseButtonDown(0)) {
            int layerMask = 1 << 9;
            if(mouseCheckCollision(layerMask)) {
                dragging = true;
                Color tcolor = rend.color;
                tcolor.a = .5f;
                rend.color = tcolor;
                //col.enabled = false;
            }
        }
        // Player lets go of key to position or put back into place
        if(Input.GetMouseButtonUp(0) && dragging) {
            dragging = false;
            //col.enabled = true;

            Color tcolor = rend.color;
            tcolor.a = 1;
            rend.color = tcolor;

            
            int layerMask = 1 << 10;
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, 0, layerMask);
            if (hit.collider != null) {
                if(hit.collider.gameObject.tag == "Key Holder") {
                    if(hit.collider.gameObject.GetComponent<HolderId>().key == key) {
                        transform.position = hit.collider.gameObject.transform.position;
                        rotateObject(-90);
                        rotatable = false;
                    }
                    else {
                        rotatable = true;
                    }
                }
                else {
                    rotatable = true;
                }
            }
            else {
                rotatable = true;
            }
        }

        // Player right clicks to rotate object
        if(Input.GetMouseButtonDown(1) && rotatable) {
            int layerMask = 1 << 9;
            if(mouseCheckCollision(layerMask)) {
                rotating = true;
            }
        }

        // Player releases right click to stop rotating
        if(Input.GetMouseButtonUp(1) && rotating) {
            rotating = false;
        }

        if(dragging) {
            transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        }

        if(rotating) {
            float angleToMouse = Mathf.Atan2(transform.position.y - mousePos.y, transform.position.x - mousePos.x) * 180 / Mathf.PI;
            print(angleToMouse);

            // 45 degrees
            if(angleToMouse > 30 && angleToMouse <= 60) {
                rotateObject(45);
            }
            // 90 degrees
            else if(angleToMouse > 60 && angleToMouse <= 120) {
                rotateObject(90);
            }
            // 135 degrees
            else if(angleToMouse > 120 && angleToMouse <= 150) {
                rotateObject(135);
            }
            // -45 degrees
            else if(angleToMouse < -30 && angleToMouse >= -60) {
                rotateObject(-45);
            }
            // -90 degrees
            else if(angleToMouse < -60 && angleToMouse >= -120) {
                rotateObject(-90);
            }
            // -135 degrees
            else if(angleToMouse < -120 && angleToMouse >= -150) {
                rotateObject(-135);
            }
            // 180 degrees
            else if(angleToMouse > 150 || angleToMouse < -150) {
                rotateObject(180);
            }
            // 0 degrees
            else {
                rotateObject(0);
            }
        }
    }

    bool mouseCheckCollision(int layerMask)
    {
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, 0, layerMask);
        if (hit.collider != null) {
            if(hit.collider.gameObject.tag == "Key") {
                if(hit.collider.gameObject.GetComponent<KeyDragAndDrop>().key == key) {
                    return true;
                }
            }
        }
        return false;
    }

    void rotateObject(float angle)
    {
        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.z = angle + 90;
        transform.rotation = Quaternion.Euler(rotationVector);
    }
}
