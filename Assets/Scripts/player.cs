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
    private bool movimiento=true;
    private SpriteRenderer sp;
    private bool vida;
    private GameObject barraVida;
    private bool attack;
    private Collider2D collider;
    public Vector3 respawn;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        sp = GetComponent<SpriteRenderer>();
        barraVida = GameObject.Find("BarraVida");
        vida = true;
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
        animator.SetBool("vida", vida);
        float h = Input.GetAxis("Horizontal");
        if (!movimiento)
        {
            h = 0;
        }
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
            collider.enabled = false;
            //StartCoroutine(enableCollider());
            StartCoroutine(waitForSec(0.6f));
        }
        if(Input.GetKeyUp(KeyCode.Return)){
            //collider.enabled = true;
            //attack = false;       
            StopCoroutine(enableCollider()); 
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = respawn;
            barraVida.SendMessage("resetVida");
            vida = true;
            jump = true;
            speed = 28f;
            JumpForce = 8f;
        }

    }

    private IEnumerator enableCollider(){
        while(attack){
            collider.enabled = false;
            yield return null;
        }
    }

    //codigo de prueba
     private IEnumerator waitForSec(float sec){
        yield return new WaitForSeconds(sec);
        collider.enabled = true;
        attack = false;
 
    }
    private void OnBecameInvisible()
    {
        
        barraVida.SendMessage("bajaVida", 100);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && attack) 
        {
            Destroy(collision.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "checkpoint"){
            respawn = collider.transform.position;
        }
    }

    public void atacado(float posEnemigo) {
        if(barraVida.GetComponent<Transform>().FindChild("Vida").localScale.x>0f){
            barraVida.SendMessage("bajaVida", 15);
            jump = true;
            float lado = Mathf.Sign(posEnemigo - transform.position.x);
            rb.AddForce(Vector2.left * lado * JumpForce, ForceMode2D.Impulse);
            movimiento = false;
            Invoke("movimientoActivado", 0.7f);
            sp.color = Color.red;
        }
        if (barraVida.GetComponent<Transform>().FindChild("Vida").localScale.x==0f)
        {
            vida = false;
            speed = 0;
            jump = false;
            JumpForce = 0;
        }

    }

    public void movimientoActivado() {
        movimiento = true;
        sp.color = Color.white;
    }

}
