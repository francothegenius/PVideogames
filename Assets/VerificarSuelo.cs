using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificarSuelo : MonoBehaviour
{
    private player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Suelo") {
            player.pisando = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Suelo")
        {
            player.pisando = false;
        }
    }

}
