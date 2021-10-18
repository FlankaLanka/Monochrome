using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    public float collisionRadius = 0.25f;
    public Vector2 bottom, right, left;
    public bool onGround = false;
    public bool wallOnLeft = false;
    public bool wallOnRight = false;
    public bool onWall = false;

    private int groundLayer;

    void Start()
    {
        groundLayer = 1 << LayerMask.NameToLayer("Ground");
    }

    void Update()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottom, collisionRadius, groundLayer);
        wallOnRight = Physics2D.OverlapCircle((Vector2)transform.position + right, collisionRadius, groundLayer);
        wallOnLeft = Physics2D.OverlapCircle((Vector2)transform.position + left, collisionRadius, groundLayer);
        onWall = wallOnLeft || wallOnRight;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere((Vector2)transform.position + bottom, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + right, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + left, collisionRadius);
    }
}
