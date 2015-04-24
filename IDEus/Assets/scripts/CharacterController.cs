using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	void Move(int blocks)
	{
		transform.Translate (new Vector3 (12, 0, 0));
	}
	void Jump(int direction)
	{	
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		if (rb.velocity.magnitude < 0.01)
			rb.AddForce (new Vector2 (direction*3f, 3f), ForceMode2D.Impulse);
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space))
			Jump (-1);
		if (Input.GetKeyDown (KeyCode.UpArrow))
			Jump (1);
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		rb.AddForce (new Vector2(1f,0));

	}
}
