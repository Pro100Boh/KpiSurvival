using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActorBehaviour<A> : MonoBehaviour where A : Actor
{
    private A actor;

    public ActorBehaviour<A> Assign(A actor)
    {
        this.actor = actor;
        return this;
    }

    public A Actor()
    {
        return actor;
    }
}
