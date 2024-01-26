using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rb;
    private Vector2 movement;
    public float speed = 6;
    public float dodgeVelocity = 50;
    public int dodgePenalty = 80;
    public float penaltyDuration = 0.3f;

    private Boolean isDodgeSlowed = false;
    private float endDodgePenalty = 0;


    // Update is called once per frame
    void Update()
    {
        
        if (Time.time > endDodgePenalty)
        {
            isDodgeSlowed = false;
           
        }

        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        movement.Normalize();

        Vector2 moveVelocity = movement * speed;
        moveVelocity.x = moveVelocity.x - (isDodgeSlowed ? (moveVelocity.x * dodgePenalty / 100) : 0);
        moveVelocity.y = moveVelocity.y - (isDodgeSlowed ? (moveVelocity.y * dodgePenalty / 100) : 0);

        //rb.velocity = moveVelocity;
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
            if (!isDodgeSlowed)
                Dodge(movement);
    }

    public void Dodge(Vector2 position)
    {

        endDodgePenalty = 0;

        rb.velocity = Vector2.zero;

        rb.AddForce(position * dodgeVelocity * 100);

        if (Time.time > endDodgePenalty)
        {
            isDodgeSlowed = true;
            endDodgePenalty = Time.time + penaltyDuration;
        }


        
    }

}
