using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{
    private AudioSource audio;
    public GameObject winText;
    public GameObject loseText;
    public GameObject continueText;
    public GameObject fail;
    public GameObject tryAgain;
    public static Control instance = null;
    public GameObject comboText;
    public GameObject comboActivated;
    private Text text;
    private GameObject barraCombo;
    private bool oneTime = false;
    //public AudioClip audioButton;

    void Start(){

        text = comboText.GetComponent<Text>();
        barraCombo = GameObject.Find("ComboBarra");
    }

    void Update(){
        if(barraCombo.GetComponent<ComboAttack>().progreso == 100){ 
            if(!oneTime){
                comboText.SetActive(true);
                StartBlinking();
                oneTime = true;
            }
        }else{
            comboText.SetActive(false);
            StopBlinking();
        }
    }
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

    IEnumerator Blink(){
        while(true){
            switch(text.color.a.ToString()){

                case "0":
                    text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
                    yield return new WaitForSeconds(0.5f);
                    break;
                case "1":
                    text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
                    yield return new WaitForSeconds(0.5f);
                    break;
            }
        }
    }

    void StartBlinking(){
        StopCoroutine("Blink");
        StartCoroutine("Blink");
    }
    void StopBlinking(){
        oneTime = false;
        StopCoroutine("Blink");
    }

    private IEnumerator comboActivatedText(){
        comboActivated.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        comboActivated.SetActive(false);
    }

    public void finishGameFail(){
        loseText.SetActive(false);
        continueText.SetActive(false);
        fail.SetActive(true);
        tryAgain.SetActive(true);
        Invoke("RegresarMenu", 5f);
    }
}
