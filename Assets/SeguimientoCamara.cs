using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguimientoCamara : MonoBehaviour
{

    public GameObject seguir;
    public Vector2 camPosMin, camPosMax; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = seguir.transform.position.x;
        float y = seguir.transform.position.y;

        transform.position = new Vector3(Mathf.Clamp(x, camPosMin.x, camPosMax.x), 
                Mathf.Clamp(y, camPosMin.y, camPosMax.y), transform.position.z);
            
    }
}
