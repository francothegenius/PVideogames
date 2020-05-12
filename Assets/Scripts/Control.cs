using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{
    private AudioSource audio;
    public GameObject winText;
    public GameObject loseText;
    public GameObject continueText;
    public static Control instance = null;


    public void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }
    public void CambiarEscena(string escena)
    {
        SceneManager.LoadScene(escena);
        audio.Stop();
    }

    public void Win()
    {
        winText.SetActive(true);
        Time.timeScale = 0.5f;
        Invoke("RegresarMenu", 0.9f);
    }

    public void RegresarMenu()
    {
        SceneManager.LoadScene("Portada");
        Invoke("resetTime",0f);
    }

    public void Lose()
    {
        loseText.SetActive(true);
        continueText.SetActive(true);
        Time.timeScale = 0.5f;
        Invoke("resetTime", 1f);
    }

    public void resetTime()
    {
        Time.timeScale = 1f;
    }

    public void resetGame()
    {
        loseText.SetActive(false);
        continueText.SetActive(false);
    }
}
