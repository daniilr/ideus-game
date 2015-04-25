using UnityEngine;
using System.Collections;

public class TerrainGenerator : MonoBehaviour {
	public GameObject Block;
	// Use this for initialization
	static int[] getRandomArray(int arraySize)
	{
		int[] array = new int[arraySize];
		for (int i = 0; i < array.Length; i++) {
			array[i] = Random.Range(1,1);
		}
		return array;
	}
	void Start () {
		int[] level = getRandomArray(8);
		GameObject clone;
		for (int i = 0; i < level.Length; i++) {
			for(int n = 0; n < level[i]; n++){
				Debug.Log(n);
				clone = Instantiate (Block);
				clone.transform.Translate(new Vector3(i*Block.GetComponent<Collider2D>().bounds.size.x, Block.GetComponent<Collider2D>().bounds.size.y*n, 0));
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
