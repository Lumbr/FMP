﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeBullet : MonoBehaviour
{
    public void SetData(int damage, Quaternion rotation, Vector3 direction, float speed, Vector3 pos, GameObject explode)
    {
        this.explode = explode.GetComponent<Explode>();
        this.damage = damage;
        this.rotation = rotation;
        this.direction = direction;
        this.speed = speed;
        this.pos = pos;
        start = true;
    }
    public Explode explode;
    public int damage;
    public Quaternion rotation;
    public Vector3 direction;
    public float speed;
    public Vector3 pos;
    public Vector3 lastPos;
    bool start;


    private void LateUpdate()
    {

        
        if (start)
        {
            speed = speed * 100;
            transform.position = pos;
            GetComponent<Rigidbody>().velocity = direction * speed;

            transform.rotation = rotation;
            start = false;
        }
        lastPos = transform.position;
        transform.rotation.SetLookRotation(GetComponent<Rigidbody>().velocity);
    }
    private void OnCollisionEnter(Collision other)
    {
        Instantiate(explode).Wee(damage, transform.position);
        Destroy(gameObject);
    }
}
