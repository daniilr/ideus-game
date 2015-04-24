using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	void MoveForward(int blocks)
	{
		transform.Translate (new Vector3 (1.7f, 0, 0));
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.RightArrow))
			MoveForward (1);
	}
}
