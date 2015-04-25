using UnityEngine;
using System.Collections;

public class FloorTrigger : MonoBehaviour {

	public void OnTriggerExit2D(Collider2D other) {
		GameObject go = GameObject.Find("Global_CTRL");
		CharacterController characterController = (CharacterController) go.GetComponent(typeof(CharacterController));
		characterController.Restart();
	}

	public void OnTriggerStay2D(Collider2D other) {
	}

	public void OnTriggerEnter2D(Collider2D other) {
	}
}
