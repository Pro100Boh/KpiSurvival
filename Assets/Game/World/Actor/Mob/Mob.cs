using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Effects;

public class Mob : Actor
{
    public float movementSpeed = 0.6f;
    public float health = 100f;

    private MobCombat combat;
    private MobAwarenes awarenes;
    private Animator animator;

    private readonly WaitForSeconds deathDelay = new WaitForSeconds(1f);

    private const float rotation = 1.0f;
    private const int intervalRotation = 100;
    private int counter = -intervalRotation;

    void Update()
    {
        counter++;

        if (counter < 0)
            transform.Rotate(0.0f, 0.0f, Random.Range(0f, rotation));

        if (counter > 0)
            transform.Rotate(0.0f, 0.0f, Random.Range(-rotation, 0f));

        if (counter > intervalRotation)
            counter = -intervalRotation;
    }

    public override void Initialize()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        awarenes = gameObject.GetComponentInChildren<MobAwarenes>();
        gameObject.AddComponent<MobMovement>().Speed(movementSpeed).Assign(this);
        combat = gameObject.AddComponent<MobCombat>();
        combat.Assign(this);
    }

    public override void Hit(Damage damage)
    {
        if (damage.isPlayer())
        {
            Player attacker = damage.Player();
            if (awarenes.TargetInRange() == null)
            {
                awarenes.Target(attacker);
            }
            else if (damage.isPlayer())
            {
                if (!awarenes.TargetInRange().Equals(attacker))
                {

                }
            }
        }
        
        health -= damage.Value();
        if (health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    public override IEnumerator Die()
    {
        GameObject.Destroy(gameObject);
        GameObject death = GameObject.Instantiate(Game.Instance.world.mobDeath);
        death.transform.position = transform.position;
        yield return deathDelay;
        GameObject.Destroy(death);
    }

    public override Animator Animator()
    {
        return animator;
    }

    public MobCombat Combat()
    {
        return combat;
    }

    public MobAwarenes Awareness()
    {
        return awarenes;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == Game.PLAYER_TAG)
        {
            combat.Start(coll.gameObject.GetComponent<Player>());
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == Game.PLAYER_TAG)
        {
            combat.Stop(coll.gameObject.GetComponent<Player>());
        }
    }
}
