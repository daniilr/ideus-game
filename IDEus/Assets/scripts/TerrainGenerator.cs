using UnityEngine;
using System.Collections;

public class TerrainGenerator : MonoBehaviour {
	public GameObject Block;
	// Use this for initialization
	void Start () {
		GameObject clone;
		for (int i = 0; i < 10; i++) {
			clone = Instantiate (Block);
			clone.transform.position = Vector3(0,0,0);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
