using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	void Move(int blocks)
	{
		GameObject blackbox = GameObject.Find ("BlackBlock");
		transform.Translate (new Vector3 (blackbox.GetComponent<Collider2D>().bounds.size.x*blocks, 0, 0));
	}
	void Jump(int direction)
	{	
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		if (rb.velocity.magnitude < 0.01)
			rb.AddForce (new Vector2 (direction*3f, 3f), ForceMode2D.Impulse);
	}

	void Restart(){
		Application.LoadLevel(Application.loadedLevel);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space))
			Jump (-1);
		if (Input.GetKeyDown (KeyCode.UpArrow))
			Jump (1);
		if (Input.GetKeyDown (KeyCode.RightArrow))
			Move (1);
		if (Input.GetKeyDown (KeyCode.LeftArrow))
			Move (-1);
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		rb.AddForce (new Vector2(1f,0));
	}
}
