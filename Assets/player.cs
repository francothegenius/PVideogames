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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
        float h = Input.GetAxis("Horizontal");
        rb.AddForce(Vector2.right * speed * h);
        // Debug.Log(rb.velocity.x);
        float limitSpeed = Mathf.Clamp(rb.velocity.x,-maxSpeed,maxSpeed);
        rb.velocity = new Vector2(limitSpeed, rb.velocity.y);

        if (h>0.1f) {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (h < -0.1f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (Input.GetKeyDown(KeyCode.Space) && pisando) {
            jump = true;
        }
        if (jump) {
            rb.AddForce(Vector2.up *JumpForce, ForceMode2D.Impulse);
            jump = false;
        }
    }

    private void OnBecameInvisible()
    {
        transform.position = new Vector3(0, 0, 0);
    }


}
