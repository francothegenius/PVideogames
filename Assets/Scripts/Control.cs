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
    public GameObject registro;
    public GameObject submit;
    public GameObject player;
    public GameObject referenciaBoss;
    public GameObject canvas;
    public GameObject coleccionable1;
    public GameObject coleccionable2;
    public GameObject coleccionable3;
    public GameObject camara;
    public GameObject control;
    public GameObject eventSystem;
    public AudioClip nivel2Sonido;
    public GameObject health;
    public GameObject combo;
    public GameObject score;
    public GameObject bossBarra;
    private Text textHealth,textCombo, textComboActivate, textScore ;
    //public AudioClip audioButton;

    void Start(){

        textHealth = health.gameObject.GetComponent<Text>();
        textCombo=combo.gameObject.GetComponent<Text>();
        textComboActivate =comboText.gameObject.GetComponent<Text>();
        textScore = score.gameObject.GetComponent<Text>();
        //bossBarra = GameObject.Find("BossBar");
        text = comboText.GetComponent<Text>();
        barraCombo = GameObject.Find("ComboBarra");
        audio = GetComponent<AudioSource>();
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
        Invoke("resetTime", 0f);
        SceneManager.LoadScene(escena);
        canvas.GetComponent<AudioSource>().Stop();
        Destroy(canvas);
        Destroy(control);
        Destroy(eventSystem);
        Destroy(player);
        //Destroy(camara);
        audio.Stop();
    }

    public void winLevel(){
        winText.SetActive(true);
        Time.timeScale = 0.5f;
        Invoke("nextLevel", 3f);
    }
    public void nextLevel()
    {
        winText.SetActive(false);
        Time.timeScale = 1f;

        //Invoke("RegresarMenu", 0.9f);
        //Scene escena = SceneManager.;
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene(3);
        Scene escena = SceneManager.GetSceneByBuildIndex(3);
        //SceneManager.MoveGameObjectToScene(player.gameObject, escena);
        //SceneManager.MoveGameObjectToScene(Canvas.gameObject, );
        player.gameObject.transform.position = new Vector2(65,3);
        canvas.GetComponent<AudioSource>().clip = nivel2Sonido;
        canvas.GetComponent<AudioSource>().Play();
        //coleccionables
        coleccionable1.gameObject.GetComponent<SpriteRenderer>().color = new Color(0f,0f, 0f);
        coleccionable2.gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f);
        coleccionable3.gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f);
        player.gameObject.GetComponent<player>().col = 0;
        DontDestroyOnLoad(control.gameObject);
        DontDestroyOnLoad(player.gameObject);
        DontDestroyOnLoad(referenciaBoss.gameObject);
        DontDestroyOnLoad(canvas.gameObject);
        DontDestroyOnLoad(camara.gameObject);
        DontDestroyOnLoad(eventSystem.gameObject);
        textHealth.color = Color.white;
        textCombo.color = Color.white;
        textComboActivate.color = Color.white;
        textScore.color = Color.white;
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

    public void iniciarJuego() {
        Destroy(GameObject.Find("Main Camera2"));
        SceneManager.LoadScene("Juego");
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
        registro.SetActive(true);
        submit.SetActive(true);
        //Invoke("RegresarMenu", 5f);
    }
}
