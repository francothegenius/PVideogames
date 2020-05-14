using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboAttack : MonoBehaviour
{
    public Image barra;
    public float progreso , maxProgreso;
    // Start is called before the first frame update
    void Start()
    {
        progreso = 0f;
        maxProgreso = 100f;
    }

    public void subirProgreso(float cantidad) 
    {
        progreso = Mathf.Clamp(progreso+cantidad,0f, maxProgreso);
        barra.transform.localScale = new Vector2(vida/maxVida, 1);
    }
}
