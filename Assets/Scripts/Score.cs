﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int score=0;
    public Text scoreField;
    // Start is called before the first frame update
    void Start()
    {
        scoreField = GetComponent<Text>();   
    }

    // Update is called once per frame
    void Update()
    {
        scoreField.text = "Score: "+ score;
    }
}
