﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gerald : EnemyAI
{
    float count = 3;
    internal override void FixedUpdate()
    {
        base.FixedUpdate();
        if (target == null) return;
        transform.LookAt(target);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        transform.eulerAngles += new Vector3(0, 90, 0);

    }
    internal override void Attack()
    {

        if (Vector3.Distance(rb.position, target.position) < targetDist)
        {
            foreach (GameObject hurtbox in hurtboxes) { hurtbox.GetComponent<DealDamage>().damage = damage; hurtbox.GetComponent<DealDamage>().knockback = knockback; }
            anim.SetBool("Swing", true);
        }
        else
        {
            foreach (GameObject hurtbox in hurtboxes) { hurtbox.SetActive(false); }
            anim.SetBool("Swing", false);
            if (count > 0)
            {
                count -= Time.deltaTime;
            }
            else
            {
                if(Random.Range(1,10) == 1)
                {
                    anim.SetBool("Shoot", false);
                    anim.SetBool("Summon", true);
                }
                else
                {
                    anim.SetBool("Shoot", true);
                    anim.SetBool("Summon", false);
                }
                count = 3;
                
            }
            if (count < 2.5)
            {
                anim.SetBool("Shoot", false);
                anim.SetBool("Summon", false);
            }
        }
    }
    internal override void PlayerSeen()
    {
        if (Vector3.Distance(rb.position, target.position) < targetDist)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            reachedEndOfPath = true;
            return;
        }
        else
        {
            direction = (target.position - rb.position).normalized;
            direction.y = 0;
            rb.velocity += direction * speed;
            rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, speed * -10, speed * 10), rb.velocity.y, Mathf.Clamp(rb.velocity.z, speed * -10, speed * 10));
            currentWaypoint = 0;
            reachedEndOfPath = true;
            return;
        }
    }
}
