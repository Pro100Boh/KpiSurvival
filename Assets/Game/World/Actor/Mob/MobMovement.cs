﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement : ActorBehaviour<Mob>
{
    private float stepDelay;
    private float stepSpeed = 0.3F;
    private Vector2 look;
    private Quaternion prevRotation;
    private float speed;

    void FixedUpdate()
    {
        if (Actor().Awareness().TargetInRange() != null)
            HandleRotate(Actor().Awareness().TargetInRange().transform.position);
        HandleMovement();
        CapVelocity();
    }

    public void HandleRotate(Vector3 target)
    {
        Vector2 direction;
        Vector2 mobPosition = Actor().transform.position;
        direction.x = target.x - mobPosition.x;
        direction.y = target.y - mobPosition.y;
        float rad = Mathf.Atan2(direction.y, direction.x);
        float angle = rad * Mathf.Rad2Deg;
        Actor().transform.rotation = Quaternion.Lerp(prevRotation, Quaternion.Euler(new Vector3(0, 0, angle)), 0.1f);
        prevRotation = Actor().transform.rotation;
        
        float x = Mathf.Cos(rad);
        float y = Mathf.Sin(rad);
        look = new Vector2(x, y);
    }

    public void HandleMovement()
    {
        stepDelay += Time.deltaTime;
        if (stepDelay < stepSpeed)
            return;
        float variantSpeed = 15 * (7 - stepSpeed / 0.05F);

        Actor().Body().AddForce(look * variantSpeed);
        if (stepSpeed > 0.12)
            stepSpeed -= 0.025f;
    }

	public void CapVelocity()
    {
        Vector2 vel = Actor().Body().velocity;
        if (vel.x > speed)
        {
            vel.x = speed;
        }
        else if (vel.x < -speed)
        {
            vel.x = -speed;
        }
        if (vel.y > speed)
        {
            vel.y = speed;
        }
        else if (vel.y < -speed)
        {
            vel.y = -speed;
        }
        Actor().Body().velocity = vel;
    }

    public MobMovement Speed(float speed)
    {
        this.speed = speed;
        return this;
    }
}
