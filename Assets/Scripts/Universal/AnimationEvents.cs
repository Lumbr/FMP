﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public Gun2D gun2D;
    public ZapCannon zap;
    public ZapCannon2D zap2D;
    public Gun[] gun;
    public AutoGun sentry;
    public AutoGun2D sentry2D;
    public void GunDone()
    {
        foreach (Gun goon in gun)
        {
            goon.done = true;
        }

    }
    public void ZapDone()
    {
        
        if (zap != null) zap.done = true;
        if (zap2D != null) zap2D.done = true;
    }
    public void SentryDone()
    {
        if (sentry != null) sentry.done = true;
        if (sentry2D != null) sentry2D.done = true;
    }
    public void SpawnSentry()
    {

        if (sentry != null) Instantiate(sentry).transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
        if (sentry2D != null)
        {
            AutoGun2D instance = Instantiate(sentry2D);
            instance.transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
            instance.axis = GetComponent<SentryCase2D>().axis;
        }


    }
    public void Die()
    {
        Destroy(gameObject);
    }
    public void Gun2DDone()
    {
        gun2D.done = true;
    }
}