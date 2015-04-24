using UnityEngine;
using System.Collections;

public class TerrainGenerator : MonoBehaviour {
	public GameObject Block;
	// Use this for initialization
	void Start () {
		GameObject clone;
		for (int i = 0; i < 10; i++) {
			clone = Instantiate (Block);
			clone.transform.position = new Vector3(Block.transform.position.x+i ,Block.transform.position.x+i,0);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
