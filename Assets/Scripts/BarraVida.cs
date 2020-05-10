using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Image barraVida;
    public float vida, maxVida = 100f;


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

    public void resetVida() 
    {
        vida = maxVida;
        barraVida.transform.localScale = new Vector2(1,1);
    }

    public bool fullVida(){
        if(vida == 100){
            return true;
        }else{
            return false;
        }
    }

    public bool tieneVida() {
        if (vida == 0) 
        {
            return false;

        }
        else
        {
            return true;
        }
    } 

}
