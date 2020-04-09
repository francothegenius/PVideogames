using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //public float maxSpeed = 1f;
    //public float speed = 1f;
    private Rigidbody2D rb;
    public Transform objetivo;
    public float velocidad;
    private Vector3 inicio, fin;
    private Animator animator;
    private bool death;
    private bool attack;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (objetivo != null)
        {
            objetivo.parent = null;
            inicio = transform.position;
            fin = objetivo.position;
        }
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Death", death);
        animator.SetBool("attack", attack);

        //rb.AddForce(Vector2.right * speed);
        //float limitSpeed = Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed);
        //rb.velocity = new Vector2(limitSpeed, rb.velocity.y);
        if (objetivo != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, objetivo.position, velocidad * Time.deltaTime);
        }

        if (transform.position == objetivo.position)
        {
            objetivo.position = (objetivo.position == inicio) ? fin : inicio;
        }
        
        if (transform.position.x== inicio.x)
        {
            transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
        if (transform.position.x == fin.x)
        {
            transform.localScale = new Vector3(-0.8f, 0.8f, 0.8f);
        }


    }

    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            Destroy(collider.gameObject);
            attack = true;
        }
    }


}
