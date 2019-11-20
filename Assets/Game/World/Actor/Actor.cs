using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    private Rigidbody2D body;

    private new AudioSource audio;

    void Awake()
    {
        gameObject.layer = Game.ACTOR_LAYER;
        body = gameObject.AddComponent<Rigidbody2D>();
        audio = gameObject.AddComponent<AudioSource>();
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
        Initialize();
    }

    public abstract void Initialize();

    public abstract void Hit(Damage damage);

    public abstract IEnumerator Die();

    public Vector2 Forward()
    {
        return transform.right;
    }

    public Rigidbody2D Body()
    {
        return body;
    }

    public AudioSource Audio()
    {
        return audio;
    }

    public abstract Animator Animator();

}
