using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobCombat : ActorBehaviour<Mob>
{
    private float speed = 0.2f;
    private int damage = 5; 

    private float timer;
    private Player victim;

    void Start()
    {
        timer = Time.time;
    }

    void Update()
    {
        if (victim == null)
            return;
        if (Time.time - timer > speed)
        {
            new Damage(Actor(), victim, damage).Apply();
            timer = Time.time;
        }
    }

    public void Start(Player victim)
    {
        this.victim = victim;
    }

    public void Stop(Player escape)
    {
        if (escape.Equals(victim))
            victim = null;
    }
}
