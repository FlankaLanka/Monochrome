using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayerMove : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;
    private Vector2 moveVelocity;
    private bool playerCanMove = true;
    Subscription<TriggerPlayerCanMove> playerCanMoveSubscription;

    void Start()
    {
        playerCanMoveSubscription = EventBus.Subscribe<TriggerPlayerCanMove>(_OnPlayerCanMoveTrigger);
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (playerCanMove)
        {
            Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            moveVelocity = moveInput.normalized * speed;
        }
    }

    void FixedUpdate()
    {

        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    private void _OnPlayerCanMoveTrigger(TriggerPlayerCanMove e)
    {
        playerCanMove = e.playerCanMove;
    }
}
