using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vidas : MonoBehaviour
{
    public Sprite[] corazones;
    // Start is called before the first frame update
    void Start()
    {
        cambioCorazones(4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cambioCorazones(int pos)
    {
        GetComponent<Image>().sprite = corazones[pos];
    }

}
