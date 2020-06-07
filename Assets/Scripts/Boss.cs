using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour

{
	private int health=100;
	private GameObject healthBar;
	private Transform player;
	private SpriteRenderer sp;
	public GameObject coleccionable;
	public bool vidaBaja;
	private bool coleccionableCreated = false;
	// Start is called before the first frame update
	void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		healthBar = GameObject.Find("BossBar");
		vidaBaja = false;
		sp = GetComponent<SpriteRenderer>();
	}

    // Update is called once per frame
    void Update()
    {
		healthBar.GetComponent<Slider>().value = health;
    }


	public bool isFlipped = false;

	public void LookAtPlayer()
	{
		Vector3 flipped = transform.localScale;
		flipped.z *= -1f;

		if (transform.position.x < player.position.x && isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = false;
		}
		else if (transform.position.x > player.position.x && !isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = true;
		}
	}

	public void bajarVida() {
		Invoke("cambioColor", 0.8f);
		sp.color = Color.red;
		health -= 10;
		if (health <= 50) {
			GetComponent<Animator>().SetBool("enojado", true);
		}

		if (health<=0) {
			GetComponent<Animator>().SetBool("muerto", true);
			if(!coleccionableCreated){
				Instantiate(coleccionable, gameObject.transform.position, gameObject.transform.rotation);
				coleccionableCreated = true;
			}
			Destroy(gameObject,0.8f);
			Score.score += 1000;
		}
	}

	private void cambioColor(){
		sp.color = Color.white;
	}
}
