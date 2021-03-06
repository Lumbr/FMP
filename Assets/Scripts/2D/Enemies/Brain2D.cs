﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain2D : EnemyAI2D
{
    internal override void PlayerSeen()
    {
        if (target == null) { if (FindObjectOfType<CharacterController2D>() != null && FindObjectOfType<CharacterController2D>().gameObject.activeInHierarchy) target = FindObjectOfType<CharacterController2D>().gameObject.transform; else return; }
        Vector3 targetDir = (target.position - transform.position).normalized;
        Vector3 targetAngle = Quaternion.LookRotation((target.position - transform.position).normalized, Vector3.up).eulerAngles;
        if (target.gameObject.activeInHierarchy)
        {

            if (axis == "ZY")
            {
                rb.velocity = new Vector3(0, Mathf.Clamp(targetDir.y * speed, speed * -10, speed * 10), Mathf.Clamp(targetDir.z * speed, speed * -10, speed * 10));
                if (targetAngle.y >= 135 && targetAngle.y <= 225) { transform.eulerAngles = new Vector3(0, 90, 0); }
                else { transform.eulerAngles = new Vector3(0, -90, 0); }
            }
            else
            {
                rb.velocity = new Vector3(Mathf.Clamp(targetDir.x * speed, speed * -10, speed * 10), Mathf.Clamp(targetDir.y * speed, speed * -10, speed * 10), 0);
                if (targetAngle.y >= 45 && targetAngle.y <= 135) { transform.eulerAngles = new Vector3(0, 180, 0); }
                else { transform.eulerAngles = new Vector3(0, 0, 0); }
            }
        }
    }
    internal override void Attack()
    {
        foreach (GameObject hurtbox in hurtboxes) { hurtbox.GetComponent<DealDamage>().damage = damage; hurtbox.GetComponent<DealDamage>().knockback = knockback; }
    }
}
