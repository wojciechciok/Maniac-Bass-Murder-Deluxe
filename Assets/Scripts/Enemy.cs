﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy : MonoBehaviour
{
    public Transform firePoint;
    private Transform target;
    private Conductor conductor;
    public int[] beatsToShoot = { 3, 7, 11, 15 };
    public GameObject bulletPrefab;
    public float bulletForce = 20f;

    // Start is called before the first frame update
    void Start()
    {
        conductor = (Conductor)GameObject.FindObjectOfType<Conductor>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        conductor.Beat.AddListener(onBeat);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - this.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);

    }

    private void onBeat(float beatValue)
    {
        int beat = (int)beatValue;
        Debug.Log(beatValue);
        Debug.Log(beat);
        if(beatsToShoot.Contains(beat))
        {
            Shoot();
            Debug.Log("Shoot");
        }

    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}