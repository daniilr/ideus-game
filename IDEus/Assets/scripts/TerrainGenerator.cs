using UnityEngine;
using System.Collections;

public class TerrainGenerator : MonoBehaviour {
	public GameObject Block;
	// Use this for initialization
	void Start () {
		int[] level = new int[5] {1, 1, 1, 2, 1};
		GameObject clone;
		for (int i = 0; i < level.Length; i++) {
			clone = Instantiate (Block);
			clone.transform.Translate(new Vector3(i*1.5f, 0, 0));
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
