﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject bullet;
    public Transform reference;
    public GameObject player;

    public GameObject izquierda;
    public GameObject derecha;
    private bool attack = true;

    private float fireRate;
    private float nextFire;



    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        fireRate = 1.88f;
        nextFire = Time.time;

        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("attack",attack);
        shoot();
        


        if(transform.position.x > player.transform.position.x){
            transform.localScale = new Vector3(-0.64f, 0.55f, 0f);
            reference = izquierda.transform;
        }
        if(transform.position.x < player.transform.position.x){
            transform.localScale = new Vector3(0.64f, 0.55f, 0f);
            reference = derecha.transform;
        }
    }

    public void shoot(){
        attack = false;
        if(Time.time > nextFire){
            attack = true;
            Instantiate(bullet,
            reference.position,
            reference.rotation
            );
            nextFire = Time.time + fireRate;
        }
    }

        private void OnTriggerEnter2D(Collider2D collider){
            if(collider.gameObject.tag == "Player"){
                collider.SendMessage("atacado", transform.position.x);
            }
            
        }
        

}