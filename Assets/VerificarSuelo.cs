using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificarSuelo : MonoBehaviour
{
    private player player;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<player>();
        rb = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "SueloMovil")
        {
            rb.velocity = new Vector3(0f, 0f, 0f);
            player.transform.parent = collision.transform;
            player.pisando = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Suelo") {
            player.pisando = true;
        }

        if (collision.gameObject.tag == "SueloMovil")
        {
            player.transform.parent = collision.transform;
            player.pisando = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Suelo")
        {
            player.pisando = false;
        }
        if (collision.gameObject.tag == "SueloMovil")
        {
            player.transform.parent = null;
            player.pisando = false;
        }
    }

}
