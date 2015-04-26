using UnityEngine;
using System.Collections;

public class TerrainGenerator : MonoBehaviour {
	public GameObject Block;

	static int[] getRandomArray(int arraySize)
	{
		int[] array = new int[arraySize];
		for (int i = 0; i < array.Length; i++) {
			array[i] = Random.Range(1,4);
		}
		return array;
	}
	void Start () {
		int[] level = getRandomArray(Random.Range(8,13));
		Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/Block1") ;
		GameObject clone;
		Debug.Log ("Size sprites " + sprites.Length);
		for (int i = 0; i < level.Length; i++) {
			for(int n = 0; n < level[i]; n++){

				clone = Instantiate (Block);
				clone.transform.Translate(new Vector3(i*Block.GetComponent<Collider2D>().bounds.size.x, Block.GetComponent<Collider2D>().bounds.size.y*n, 0));
				//clone.AddComponent<SpriteRenderer>();
				//clone.GetComponent<SpriteRenderer>().sprite = sprites[1];
				if (i == level.Length-1){
					GameObject winBox = GameObject.Find("winBox");
					Debug.Log("WinBox x position: "+clone.transform.position.x);
					winBox.transform.position = new Vector3(clone.transform.position.x,0,0);
				}
				}
			}
		}


	
	// Update is called once per frame
	void Update () {
	
	}
}
