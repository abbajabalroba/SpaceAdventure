﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlyControl : MonoBehaviour
{
    public Rigidbody2D playerRb;
    private float force;
    private float incrementalForce;
    private float maxForce;
    private float normalSpeed;
    private float slowSpeed;
    private bool isBraking;

    void Start()
    {
        force = 0;
        incrementalForce = 200f;
        maxForce = 800f;
        normalSpeed = 3f;
        slowSpeed = 0.5f;
        isBraking = false;
    }

    void Update()
    {
        BrakeControl();
        FlyControl();
        HorizantalMovementControl();
    }

    void HorizantalMovementControl()
    {
        if (Input.GetKey("left") && !isBraking)
        {
            //transform.Rotate(0,0,0.5f);
            transform.Translate(-normalSpeed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey("left") && isBraking)
        {
            transform.Translate(-slowSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey("right") && !isBraking)
        {
            //transform.Rotate(0,0,-0.5f);
            transform.Translate(normalSpeed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey("right") && isBraking)
        {
            transform.Translate(slowSpeed * Time.deltaTime, 0, 0);
        }
    }

    void BrakeControl()
    {
        if (Input.GetKey("down"))
        {
            playerRb.velocity = Vector2.zero;
            force = 0;
            playerRb.gravityScale = 0;
            isBraking = true;
        }
        if (Input.GetKeyUp("down"))
        {
            playerRb.gravityScale = 1;
            isBraking = false;
        }
    }

    void FlyControl()
    {
        if (Input.GetKey("up") && !isBraking)
        {
            force += incrementalForce;
        }
        else if (Input.GetKeyUp("up") && !isBraking)
        {
            force = 0;
        }
        if (force > maxForce)
        {
            force = maxForce;
        }

        playerRb.AddForce(Vector2.up * force * Time.deltaTime);
    }
}
