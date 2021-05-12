﻿using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float damageMultiplier;
    public List<GameObject> hits;
    public int damage;
    float count;
    public float timer;
    bool dealDamage;
    private void OnEnable()
    {
        count = timer;
    }
    private void Update()
    {
        count -= Time.deltaTime;
        if (count <= 0)
        {
            dealDamage = true;
            count = timer;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Health>() != null && dealDamage) { foreach (GameObject hit in hits) hit.gameObject.GetComponent<Health>().TakeDamage(damage); dealDamage = false; }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Enemy2D") hits.Add(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Enemy2D") hits.Remove(other.gameObject);
    }
}
