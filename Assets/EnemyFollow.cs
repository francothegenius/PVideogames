using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    //public float maxSpeed = 1f;
    //public float speed = 1f;
    private Rigidbody2D rb;
    private Transform player;
    public float velocidad;
    private Vector3 inicio, fin;
    private Animator animator;
    private bool death;
    private bool attack;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();  
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Death", death);
        animator.SetBool("attack", attack);

        //rb.AddForce(Vector2.right * speed);
        //float limitSpeed = Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed);
        //rb.velocity = new Vector2(limitSpeed, rb.velocity.y);

        transform.position = Vector2.MoveTowards(transform.position, player.position, velocidad * Time.deltaTime);


    }

    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            Destroy(collider.gameObject);
            attack = true;
        }
    }


}
