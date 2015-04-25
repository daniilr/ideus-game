using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {
	const int FRONT = 1;
	const int BACK = -1;
	//object[][] action_queue = new string[] {new string[]{"Jump", FRONT}, new string[]{"Jump", BACK}};
	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	void Move(int blocks)
	{
		GameObject blackbox = GameObject.Find ("BlackBlock");
		transform.Translate (new Vector3 (blackbox.GetComponent<Collider2D>().bounds.size.x*blocks, 0, 0));
	}
	void Jump(int direction)
	{	
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
//		if (action_queue.Length > 0 && rb.velocity.magnitude < 0.01) {
//
//		}

	}
}
