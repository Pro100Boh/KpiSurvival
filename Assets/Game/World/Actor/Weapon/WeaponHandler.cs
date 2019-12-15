using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    private float nextHit;
    private Player player;
    private WeaponContainer container;

    public ParticleSystem blood;

    void Start()
    {
        player = GetComponentInParent<Player>();
        container = player.Weapons();
    }

    void Update()
    {
        if (container.Selected() == null)
            return;

        if (container.ShootAttempt() && Time.time > nextHit)
        {
            if (!container.Selected().canAttack(player, container))
                return;
            nextHit = Time.time + container.Selected().rate;
            StartCoroutine(container.Selected().Effect());
            container.Selected().Attack(player, blood);
            container.UpdateSelected();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RangedWeapon rangedWeapon = container.Selected() as RangedWeapon;
            if (rangedWeapon != null)
            {
                rangedWeapon.TryReload(player, container);
            }
        }
    }
}