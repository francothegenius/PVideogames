﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public Sprite redCheck;
    public Sprite greenCheck;
    private SpriteRenderer checkpointRenderer;
    public bool checkReach;
    // Start is called before the first frame update
    void Start()
    {
        checkpointRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.tag == "Player"){
            checkpointRenderer.sprite = greenCheck;
            checkReach = true;
        }
        //Debug.Log("Entro");
    }
}