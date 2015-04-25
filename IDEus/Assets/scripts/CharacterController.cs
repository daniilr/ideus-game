using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {
	const int FRONT = 1;
	const int BACK = -1;
	Rigidbody2D rb;
	int char_direction = FRONT;
	private Animator anim;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator> ();
	}

	void Move(int direction)
	{
		if (char_direction != direction) {
			Flip ();
			char_direction *= -1;
		}
		GameObject blackbox = GameObject.Find ("BlackBlock");
		transform.position = Vector3.Lerp (transform.position, transform.position + new Vector3 (blackbox.GetComponent<Collider2D>().bounds.size.x*direction, 0, 0), 0.3f);
		//transform.Translate (new Vector3 (blackbox.GetComponent<Collider2D>().bounds.size.x*direction, 0, 0));
		Callback(System.Reflection.MethodBase.GetCurrentMethod().Name);
	}

	void Jump(int direction)
	{	
		if (rb.velocity.magnitude < 0.01)
			rb.AddForce (new Vector2 (direction*3f, 3f), ForceMode2D.Impulse);
		Callback(System.Reflection.MethodBase.GetCurrentMethod().Name);
	}

	void Restart(){
		Application.LoadLevel(Application.loadedLevel);
		Callback(System.Reflection.MethodBase.GetCurrentMethod().Name);
	}
	void Flip(){
		transform.Rotate(new Vector3(0, (transform.rotation.y == 180) ? 0 : 180, 0));
	}
	void Callback(string eventName){
		Application.ExternalCall( "callbackEvent", eventName);
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space))
			Jump (BACK);
		if (Input.GetKeyDown (KeyCode.UpArrow))
			Jump (FRONT);
		if (Input.GetKeyDown (KeyCode.RightArrow))
			Move (FRONT);
		if (Input.GetKeyDown (KeyCode.LeftArrow))
			Move (BACK);
		if (Input.GetKeyDown (KeyCode.F))
			Flip ();
		float axis = Input.GetAxis("Horizontal");
		anim.SetFloat ("Speed", Mathf.Abs(axis));
		if (axis != 0) {
			transform.Translate(new Vector3(axis*-char_direction*0.01f,0,0));
		}
	}
}
