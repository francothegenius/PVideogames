using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscalonMovil : MonoBehaviour
{
    public Transform objetivo;
    public float velocidad;
    private Vector3 inicio, fin;
    // Start is called before the first frame update
    void Start()
    {
        if (objetivo != null)
        {
            objetivo.parent = null;
            inicio = transform.position;
            fin = objetivo.position;
        }   
    }

    // Update is called once per frame
    void Update()
    {
        if (objetivo != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, objetivo.position, velocidad * Time.deltaTime);
        }

        if (transform.position == objetivo.position)
        {
            objetivo.position = (objetivo.position == inicio) ? fin : inicio;
        }
    }
}
