using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour

{
    private AudioSource audio;
    private Button button { get { return GetComponent<Button>(); } } 
    //private AudioClip audioButton;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void playSound()
    {
        audio.Play();
    }
}
