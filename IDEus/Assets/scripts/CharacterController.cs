using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterController : MonoBehaviour {
	const int FRONT = 1;
	const int BACK = -1;
	Rigidbody2D rb;
	int char_direction = FRONT;
	private Animator anim;
	private List<Vector3> moveQueue;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator> ();
		moveQueue = new List<Vector3> ();
	}

	void Move(int direction)
	{
		if (char_direction != direction)
			Flip ();
		GameObject blackbox = GameObject.Find ("BlackBlock");
		Vector3 dest = transform.position + new Vector3 (blackbox.GetComponent<Collider2D>().bounds.size.x*direction, 0, 0);
		moveQueue.Add (dest);
		Debug.Log (moveQueue.Count);
		//transform.Translate (new Vector3 (blackbox.GetComponent<Collider2D>().bounds.size.x*direction, 0, 0));
		Callback(System.Reflection.MethodBase.GetCurrentMethod().Name);
	}

	void Jump(int direction)
	{	
		if (direction != char_direction)
			Flip ();
		if (rb.velocity.magnitude < 0.01)
			rb.AddForce (new Vector2 (direction*3f, 3f), ForceMode2D.Impulse);
		Callback(System.Reflection.MethodBase.GetCurrentMethod().Name);
	}

	void Restart(){
		Application.LoadLevel(Application.loadedLevel);
		Callback(System.Reflection.MethodBase.GetCurrentMethod().Name);
	}
	void Flip(){
		char_direction *= -1;
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
		//anim.SetFloat ("Speed", Mathf.Abs(axis));
		if (moveQueue.Count > 0) {
			anim.SetFloat ("Speed", 0.9f);
			transform.position = Vector3.Lerp (transform.position, moveQueue [0], Time.deltaTime * 0.8f);
			if (Mathf.Abs (transform.position.x - moveQueue [0].x) < 0.3) {
				moveQueue.RemoveAt (0);
			}
		} else
			anim.SetFloat ("Speed", 0);
		if (axis != 0) {
			transform.Translate(new Vector3(axis*-char_direction*0.02f,0,0));
		}
	}
}
