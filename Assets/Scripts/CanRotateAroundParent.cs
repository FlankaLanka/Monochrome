using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanRotateAroundParent : MonoBehaviour
{
    public Transform target;
    //public float fRadius = 3.0f;
    private Transform pivot;
    private Rigidbody2D rb2D;
    private Collider2D col2D;

    void Start()
    {
        pivot = new GameObject().transform;
        pivot.position = target.position;
        rb2D = GetComponent<Rigidbody2D>();
        col2D = GetComponent<Collider2D>();

        transform.parent = pivot;
    }

    void Update()
    {
        Vector3 v3Pos = Camera.main.WorldToScreenPoint(target.position);
        v3Pos = Input.mousePosition - v3Pos;
        float angle = Mathf.Atan2(v3Pos.y, v3Pos.x) * Mathf.Rad2Deg;

        pivot.position = target.position;
        pivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
