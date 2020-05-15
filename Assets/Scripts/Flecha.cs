using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    private GameObject barraCombo;

    private void Start()
    {
        barraCombo = GameObject.Find("ComboBarra");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy"){
            barraCombo.SendMessage("subirProgreso", 25);
            Destroy(collision.gameObject, 0.7f);
            collision.gameObject.SendMessage("estadoMuerte");
            Destroy(gameObject);

        }
        else{
            Destroy(gameObject);
        }
        
    }
}
