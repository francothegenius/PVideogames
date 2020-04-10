using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float speed = 2f;
    public bool pisando;
    public float JumpForce = 6.5f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool jump;
    private bool doubleJump;

    private bool attack;
    private Collider2D collider;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 nuevaVelocidad = rb.velocity;
        nuevaVelocidad.x *= 0.75f;
        if (pisando)
        {
            rb.velocity = nuevaVelocidad;
        }
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("Pisando", pisando);
        animator.SetBool("attack", attack);
        float h = Input.GetAxis("Horizontal");
        rb.AddForce(Vector2.right * speed * h);
        // Debug.Log(rb.velocity.x);
        float limitSpeed = Mathf.Clamp(rb.velocity.x,-maxSpeed,maxSpeed);
        rb.velocity = new Vector2(limitSpeed, rb.velocity.y);

        if (h>0.1f) {
            transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
        if (h < -0.1f)
        {
            transform.localScale = new Vector3(-0.8f, 0.8f, 0.8f);
        }

        if (pisando) {
            doubleJump = true;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (pisando)
            {
                jump = true;
                doubleJump = true;
            }
            else if (doubleJump) {
                jump = true;
                doubleJump = false;
            }
        }
        if (jump) {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up *JumpForce, ForceMode2D.Impulse);
            jump = false;
        }

        if(Input.GetKeyDown(KeyCode.Return)){
            attack = true;
            StartCoroutine(enableCollider());
        }
        if(Input.GetKeyUp(KeyCode.Return)){
            collider.enabled = true;
            attack = false;       
            StopCoroutine(enableCollider()); 
        }

    }

    private IEnumerator enableCollider(){
        while(attack){
            collider.enabled = false;
            yield return null;
        }
    }







    private void OnBecameInvisible()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && attack) 
        {
            Destroy(collision.gameObject);
        }
    }

}
