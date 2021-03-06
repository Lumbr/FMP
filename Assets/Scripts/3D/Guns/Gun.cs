﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Gun : GunBase
{
    public float critChance;
    public GameObject player;
    public Slider ammoSlider;
    float reloadCount;
    public float reloadTime;
    public ParticleSystem[] steam;
    public GameObject bullet;
    public Explode explode;
    public ParticleSystem muzzle;
    public float speed;
    public Transform firePoint;
    public Animator anim;
    public int clipSize;
    int ammo;
    bool steaming;
    InputMaster input;
    void Start()
    {
        ammo = clipSize;
    }
    private void OnEnable()
    {
        input = new InputMaster();
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
    }
    void Update()
    {
        
        if (ammo <= 0 && !(reloadTime <= 0))
        {
            anim.SetBool("Shoot", false);
            ammoSlider.value = ammoSlider.maxValue;
            Reload(reloadTime);
        }
        else
        {
            ammoSlider.maxValue = clipSize;
            ammoSlider.value = ammo;
        }
        anim.SetInteger("WepNum", wepNum);
        if (done&&!steaming)
        {

            anim.SetBool("Shoot", false);
            if (InputSystem.GetDevice<Mouse>().leftButton.isPressed)
            {
                ammo--;
                done = false;
                switch (wepNum)
                {
                    case 0: FirePistol(); break;
                    case 1: FireShotgun(); break;
                    case 2: FireBoomer(); break;
                    case 3: FireExploder(); break;
                    case 6: ThrowSentry(); break;


                }


            }


        }
    }
    void Reload(float reloadTime)
    {
        if (!steaming)
        {
            reloadCount = reloadTime;
            foreach (ParticleSystem current in steam) { current.Play(); }
            steaming = true;
        }
        reloadCount -= Time.deltaTime;
        if (reloadCount <= 0)
        {
            steaming = false;
            ammo = clipSize;
            done = true;
        }
    }
    void FirePistol()
    {

        muzzle?.Play();
        Camera.main.gameObject.GetComponentInParent<AudioManager>().sfx[0].Play();
        StartCoroutine(Camera.main.GetComponentInParent<CameraShake>().Shake(0.1f, 0.2f));
        Instantiate(bullet, firePoint.position, Quaternion.Euler(0, 0, 0)).GetComponent<Bullet>().SetData((int)Mathf.Ceil(damage * player.GetComponent<Stats>().baseDamage * damageMultiplier), player.GetComponent<Stats>().critChance, firePoint.rotation, firePoint.forward, speed, firePoint.position);
        anim.SetBool("Shoot", true);


    }
    void FireShotgun()
    {
        Camera.main.gameObject.GetComponentInParent<AudioManager>().sfx[1].Play();
        StartCoroutine(Camera.main.GetComponentInParent<CameraShake>().Shake(0.2f, 0.4f));
        muzzle?.Play();
        for (int i = 0; i <= 6; i++) Instantiate(bullet).GetComponent<Bullet>().SetData((int)Mathf.Ceil(damage * player.GetComponent<Stats>().baseDamage * damageMultiplier), player.GetComponent<Stats>().critChance, firePoint.rotation, InaccuracyCalc(), speed, firePoint.position);
        anim.SetBool("Shoot", true);

    }
    void FireBoomer()
    {
        Camera.main.gameObject.GetComponentInParent<AudioManager>().sfx[2].Play();
        muzzle?.Play();
        Instantiate(bullet,firePoint.position,Quaternion.Euler(0,0,0)).GetComponent<Boomerang>().SetData((int)Mathf.Ceil(damage * player.GetComponent<Stats>().baseDamage * damageMultiplier), player.GetComponent<Stats>().critChance, firePoint.rotation, firePoint.forward, speed, firePoint.position, gameObject);
        anim.SetBool("Shoot", true);


    }
    void FireExploder()
    {
        Camera.main.gameObject.GetComponentInParent<AudioManager>().sfx[2].Play();
        muzzle?.Play();
        Instantiate(bullet, firePoint.position, Quaternion.Euler(0, 0, 0)).GetComponent<ExplodeBullet>().SetData((int)Mathf.Ceil(damage * player.GetComponent<Stats>().baseDamage * damageMultiplier), player.GetComponent<Stats>().critChance, firePoint.rotation, firePoint.forward, speed, firePoint.position, explode);
        anim.SetBool("Shoot", true);

    }
    void ThrowSentry()
    {
        Camera.main.gameObject.GetComponentInParent<AudioManager>().sfx[3].Play();
        Instantiate(bullet, firePoint.position, Quaternion.Euler(0, 0, 0)).GetComponent<SentryCase>().SetData((int)Mathf.Ceil(damage * player.GetComponent<Stats>().baseDamage * damageMultiplier), player.GetComponent<Stats>().critChance, firePoint.rotation, firePoint.forward, speed, firePoint.position);
        anim.SetBool("Shoot", true);
    }
    Vector3 InaccuracyCalc() { return new Vector3(firePoint.forward.x + (firePoint.forward.x * Random.Range(-0.1f, 0.1f)), firePoint.forward.y + (firePoint.forward.y * Random.Range(-0.3f, 0.3f)), firePoint.forward.z + (firePoint.forward.z * Random.Range(-0.1f, 0.1f))).normalized; }
}
