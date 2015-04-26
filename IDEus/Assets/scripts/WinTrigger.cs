using UnityEngine;
using System.Collections;

public class WinTrigger : MonoBehaviour {
	
	public void OnTriggerExit2D(Collider2D other) {
		Debug.Log("OnTriggerExit2D WinTrigger");
	}
	
	public void OnTriggerStay2D(Collider2D other) {
		Debug.Log("OnTriggerStay2D WinTrigger");
	}
	
	public void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("OnTriggerEnter2D WinTrigger");

		GameObject go = GameObject.Find("Global_CTRL");
		CharacterController characterController = (CharacterController) go.GetComponent(typeof(CharacterController));
		characterController.Win();
	}
}
