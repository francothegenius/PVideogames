using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float speed = 2f;
    public bool pisando;
    public float JumpForce = 6.5f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool jump;
    private bool doubleJump;
    private bool movimiento=true;
    private SpriteRenderer sp;
    private bool vida;
    private GameObject barraVida;
    private GameObject barraCombo;
    private bool attack;
    private bool attack2;
    private bool attack2enabled;
    private bool comboAttack1;
    public GameObject flechaPrefab;
    public Transform referenceFlecha;
    public float fuerzaFlecha;
    private Collider2D collider;
    private Vector3 respawn;
    private AudioSource audioPlayer;
    public AudioClip audioCaminar;
    public AudioClip audioSaltar;
    public AudioClip audioSaltarDoble;
    public AudioClip audioAtacar;
    public AudioClip audioAtacado;
    public AudioClip audioMorir;
    public AudioClip audioVida;
    public AudioClip audioWilhelm;
    public AudioClip audioDisparar;
    public AudioClip audioArco;
    public AudioClip audioCombo;
    public AudioClip comboActivated;

    public bool isMoving=false;
    private GameObject control;
    public AudioClip audioCaminarRoca;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        sp = GetComponent<SpriteRenderer>();
        barraVida = GameObject.Find("BarraVida");
        barraCombo = GameObject.Find("ComboBarra");
        vida = true;
        audioPlayer = GetComponent<AudioSource>();
        fuerzaFlecha = 12f;
        control = GameObject.Find("Control");
    }

    // Update is called once per frame
    void Update()
    {
        //asignacion velocidad maxima para evitar que el personaje
        //acumule velocidad
        Vector3 nuevaVelocidad = rb.velocity;
        nuevaVelocidad.x *= 0.75f;
        if (pisando)
        {
            rb.velocity = nuevaVelocidad;
        }
        //animators
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("Pisando", pisando);
        animator.SetBool("attack", attack);
        animator.SetBool("attack_sec", attack2);
        animator.SetBool("vida", vida);
        animator.SetBool("combo_attack_1", comboAttack1);


        //moviemiento personaje
        float h = Input.GetAxis("Horizontal");
        if (!movimiento)
        {
            h = 0;
        }
        rb.AddForce(Vector2.right * speed * h);
        float limitSpeed = Mathf.Clamp(rb.velocity.x,-maxSpeed,maxSpeed);
        rb.velocity = new Vector2(limitSpeed, rb.velocity.y);
    
        if (h!= 0)
        {
            isMoving = true;
        }
        else {
            isMoving = false;
        }

        //audio pasos
        if (isMoving && pisando)
        {
            //audioPlayer.clip = audioCaminar;
            if (!audioPlayer.isPlaying)
            {
                audioPlayer.PlayOneShot(audioCaminar);
            }
        }

        //mover personaje segun dirección
        if (h>0.1f) {
            transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
        if (h < -0.1f)
        {
            transform.localScale = new Vector3(-0.8f, 0.8f, 0.8f);
        }

        //brincar
        if (pisando) {
            doubleJump = true;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (pisando)
            {
                //audioPlayer.clip = audioSaltar;
                audioPlayer.PlayOneShot(audioSaltar);
                jump = true;
                doubleJump = true;
            }
            else if (doubleJump) {
                //audioPlayer.clip = audioSaltarDoble;
                audioPlayer.PlayOneShot(audioSaltarDoble);
                jump = true;
                doubleJump = false;
            }

        }
        if (jump) {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up *JumpForce, ForceMode2D.Impulse);
            jump = false;
        }

        //ataque
        if(Input.GetKeyDown(KeyCode.Return)){
            //ataque secundario
            if(attack2enabled){
                shootFlecha();
                StartCoroutine(desactivarAtaqueSec(0.3f));
            }
            //ataque primario
            else if(vida){
            attack = true;
            collider.enabled = false;
            audioPlayer.PlayOneShot(audioAtacar);
            StartCoroutine(enableCollider(0.6f));
            }

        }

        //combo attack
        if(Input.GetKeyDown(KeyCode.F)){
            if(barraCombo.GetComponent<ComboAttack>().progreso == 100){
                //texto
                audioPlayer.PlayOneShot(comboActivated);
                control.GetComponent<Control>().comboText.SetActive(false);
                control.SendMessage("StopBlinking");
                barraCombo.SendMessage("resetBarraProgeso");
                control.SendMessage("comboActivatedText");
                comboAttack1 = true;
                collider.enabled = false;
                StartCoroutine(enableCollider(3.2f));
                audioPlayer.PlayOneShot(audioCombo);
            }

        }

        //respawn
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = respawn;
            barraVida.SendMessage("resetVida");
            vida = true;
            jump = true;
            speed = 28f;
            JumpForce = 8f;
            Control.instance.resetGame();
        }

    }

    //metodo utilizado para activar collider despues de ataque
    //es necesitado ya que el ataque es cercano a enemigo
    //y si no se desactiva collider de jugador, muere al ser colisionado con enemigo
    private IEnumerator enableCollider(float sec){
        yield return new WaitForSeconds(sec);
        collider.enabled = true;
        attack = false;
        comboAttack1 = false;
    }

    //desactivar ataque secundario
    private IEnumerator desactivarAtaqueSec(float sec){
        yield return new WaitForSeconds(sec);
        attack2 = false;
    }


    //cuando colisiona con el enemigo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && (attack || comboAttack1))
        {
            Destroy(collision.gameObject, 0.7f);
            collision.gameObject.SendMessage("estadoMuerte");
            barraCombo.SendMessage("subirProgreso", 25);
        }
    }

    //checkpoint
    //HP
    //arco
    //limite cuando cae
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "checkpoint"){
            respawn = collider.transform.position;
        }
        if(collider.gameObject.tag == "HP" && barraVida.GetComponent<BarraVida>().vida != 100){
            barraVida.SendMessage("resetVida");
            audioPlayer.PlayOneShot(audioVida);
            Destroy(collider.gameObject);
            Invoke("movimientoActivado", 0.7f);
            sp.color = Color.green;
        }
        if(collider.gameObject.tag == "arco"){  
            attack2enabled = true;
            Destroy(collider.gameObject);
            audioPlayer.PlayOneShot(audioArco);
        }
        if(collider.gameObject.tag == "limite"){
            audioPlayer.PlayOneShot(audioWilhelm);
            barraVida.SendMessage("bajaVida", 100);
            barraCombo.SendMessage("resetBarraProgeso");
            Control.instance.Lose();
        }
    }

    //metodo llamado desde enemigo
    //baja vida
    //acciona al ser atacado
    public void atacado(float posEnemigo) {
        if(barraVida.GetComponent<Transform>().Find("Vida").localScale.x>0f){
            barraVida.SendMessage("bajaVida", 15);
            barraCombo.SendMessage("resetBarraProgeso");
            jump = true;
            float lado = Mathf.Sign(posEnemigo - transform.position.x);
            rb.AddForce(Vector2.left * lado * JumpForce, ForceMode2D.Impulse);
            movimiento = false;
            //audioPlayer.clip = audioAtacado;
            audioPlayer.PlayOneShot(audioAtacado);
            Invoke("movimientoActivado", 0.7f);
            sp.color = Color.red;
        }
        if (barraVida.GetComponent<Transform>().Find("Vida").localScale.x==0f)
        {
            //audioPlayer.clip = audioMorir;
            audioPlayer.PlayOneShot(audioMorir);
            Control.instance.Lose();
            vida = false;
            speed = 0;
            jump = false;
            JumpForce = 0;
            attack2enabled = false;
            barraCombo.SendMessage("resetBarraProgeso");

        }

    }

    //activar movimiento al jugador despues de ser atacado y regresa a color original
    public void movimientoActivado() {
        movimiento = true;
        sp.color = Color.white;
    }

    //disparo de flechas
    //ataque secundario
    void shootFlecha()
    {
        attack2 = true;
        GameObject flecha = Instantiate(flechaPrefab, referenceFlecha.position, referenceFlecha.rotation);
        Rigidbody2D rb = flecha.GetComponent<Rigidbody2D>();
        rb.AddForce(referenceFlecha.up * fuerzaFlecha, ForceMode2D.Impulse);
        audioPlayer.PlayOneShot(audioDisparar);
        Destroy(flecha, 2f);
    }

}
