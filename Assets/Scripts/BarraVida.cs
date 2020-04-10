using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Image barraVida;
    float vida, maxVida = 100f;
    // Start is called before the first frame update
    void Start()
    {
        vida = maxVida;
    }

    public void bajaVida(float cantidad) 
    {
        vida = Mathf.Clamp(vida-cantidad,0f, maxVida);
        barraVida.transform.localScale = new Vector2(vida/maxVida, 1);
    }
}
