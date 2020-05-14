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
        barra.GetComponent<Image>().color = new Color32(232, 238, 53, 255);
        progreso = Mathf.Clamp(progreso+cantidad,0f, maxProgreso);
        barra.transform.localScale = new Vector2(progreso/maxProgreso, 1);
    }

    public void resetBarraprogeso()
    {
        barra.GetComponent<Image>().color = new Color32(149,149, 149, 255);
        progreso = 0f;
        barra.transform.localScale = new Vector2(1, 1);
    }
}
