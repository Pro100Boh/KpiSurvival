using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : ActorBehaviour<Player>
{
    private const int STEP_AUDIO_DELAY = 14;
    private const string STEP_CLIPS_NAME = "footstep";
    private const int STEP_CLIPS_LENGTH = 9;

    private const float MAX_VELOCITY = 1.4f;
    private const float STRAIGHT_CAP = 0.1f;
    private const float CURVED_CAP = 0.35f;
    private const float CURVED_DIVIDER = 1.05f;

    private const int IDDLE_ANIM = 0;
    private const int WALK_ANIM = 1;

    private int stepSync;
    private float stepDelay;
    private float stepSpeed = 0.5F;

    private bool inMovement = false;

    private Vector2 look;

    private Quaternion prevRotation;

    void FixedUpdate()
    {
        HandleRotate();
        HandleMovement();
        CapVelocity();
    }

    public void HandleRotate()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        Vector3 objectPos = Game.Instance.world.camera.WorldToScreenPoint(Actor().transform.position);
        mousePos.x -= objectPos.x;
        mousePos.y -= objectPos.y;
        float rad = Mathf.Atan2(mousePos.y, mousePos.x);
        float angle = rad * Mathf.Rad2Deg;
        Actor().transform.rotation = Quaternion.Lerp(prevRotation, Quaternion.Euler(new Vector3(0, 0, angle)), 0.5f);
        prevRotation = Actor().transform.rotation;
        
        float x = Mathf.Cos(rad);
        float y = Mathf.Sin(rad);
        if (x < STRAIGHT_CAP && x > -STRAIGHT_CAP)
            x = 0;
        else if (x < CURVED_CAP && x > -CURVED_CAP)
            x /= 10f;
        if (y < STRAIGHT_CAP && y > -STRAIGHT_CAP)
            y = 0;
        else if (y < CURVED_CAP && y > -CURVED_CAP)
            y /= 10f;
        look = new Vector2(x, y);
    }

    public void HandleMovement()
    {
        stepDelay += Time.deltaTime;
        if (stepDelay < stepSpeed)
            return;
        inMovement = true;

        var moveForce = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
            moveForce += Vector2.up;

        if (Input.GetKey(KeyCode.S))
            moveForce += Vector2.down;

        if (Input.GetKey(KeyCode.A))
            moveForce += Vector2.left;

        if (Input.GetKey(KeyCode.D))
            moveForce += Vector2.right;

        if (!moveForce.Equals(Vector2.zero))
        {
            AddFroceToActor(moveForce);
        }
        else
        {
            inMovement = false;
            stepSpeed = 0.3F;
            stepSync = 0;
            Actor().Animator().SetInteger("State", IDDLE_ANIM);
        }

        if (inMovement && stepSync++ > STEP_AUDIO_DELAY)
        {
            stepDelay = 0;
            AudioClip step = Game.Instance.audio.Get(STEP_CLIPS_NAME + Random.Range(0, STEP_CLIPS_LENGTH));
            Actor().Audio().PlayOneShot(step, Random.Range(0.3f, 0.7f));
            stepSync = 0;
        }
    }

    private void AddFroceToActor(Vector2 force)
    {
        float variantSpeed = 10 * (7 - stepSpeed / 0.05F);

        Actor().Body().AddForce(force * variantSpeed);
        Actor().Animator().SetInteger("State", WALK_ANIM);
        if (stepSpeed > 0.15)
            stepSpeed -= 0.025f;
    }

    public void CapVelocity()
    {
        Vector2 vel = Actor().Body().velocity;
        if (vel.x > MAX_VELOCITY)
        {
            vel.x = MAX_VELOCITY;
        }
        else if (vel.x < -MAX_VELOCITY)
        {
            vel.x = -MAX_VELOCITY;
        }
        else if (!inMovement)
        {
            vel.x = 0;
        }
        else
        {
            vel.x /= CURVED_DIVIDER;
        }
        if (vel.y > MAX_VELOCITY)
        {
            vel.y = MAX_VELOCITY;
        }
        else if (vel.y < -MAX_VELOCITY)
        {
            vel.y = -MAX_VELOCITY;
        }
        else if (!inMovement)
        {
            vel.y = 0;
        }
        else
        {
            vel.y /= CURVED_DIVIDER;
        }
        Actor().Body().velocity = vel;
    }
}
