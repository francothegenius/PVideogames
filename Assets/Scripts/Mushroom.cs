using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject bullet;
    public Transform reference;
    private bool attack = true;

    private float fireRate;
    private float nextFire;



    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        fireRate = 0.95f;
        nextFire = Time.time;
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("attack", attack);
        if(attack == true){
            shoot();
        }
    }

    public void shoot(){
        if(Time.time > nextFire){
            attack = true;
            Instantiate(bullet,
            reference.position,
            reference.rotation
            );
            nextFire = Time.time + fireRate;
        }
    }


    

}
