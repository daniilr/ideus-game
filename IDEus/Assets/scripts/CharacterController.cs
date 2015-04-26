using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct MoveAction {
	public string type;
	public Vector3 vector;
	public Vector3 target;
	public int direction;
	public bool inAction;
	public const int FRONT = 1;
	public const int BACK = -1;

	public MoveAction(string action_type){
		type = action_type;
		vector = new Vector3 (0, 0, 0);
		target = new Vector3 (0, 0, 0);
		direction = 0;
		inAction = false;
	}
	public bool needFleep(MoveAction next, int curDirection){
		if (next.type == "jump") {
			return direction != curDirection;
		} else if (next.type == "walk") {
			if (type == "walk"){
				return vector.x*next.vector.x < 0;
			} else
				return direction*vector.x < 0;

		}
	    return false;
	}
}

public class CharacterController : MonoBehaviour {
	Rigidbody2D rb;
	int char_direction = MoveAction.FRONT;
	private Animator anim;
	private List<MoveAction> moveQueue;
	private Vector3 cur_target;
	// Use this for initialization
	void Start () {
		//Camera camera = (Camera) GameObject.Find ("Main Camera").GetComponent (typeof(Camera));
		//transform.localScale = new Vector3(camera.orthographicSize/2 * (Screen.width/Screen.height),camera.orthographicSize/2,0f);
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator> ();
		moveQueue = new List<MoveAction> ();
	}

	void Move(int direction)
	{
		GameObject blackbox = GameObject.Find ("BlackBlock");
		Vector3 base_pos;
		MoveAction moveAction = new MoveAction("walk");
		if (moveQueue.Count > 0)
			base_pos = moveQueue [moveQueue.Count - 1].target;
		else
			base_pos = transform.position;

		moveAction.vector = new Vector3 (blackbox.GetComponent<Collider2D>().bounds.size.x*direction, 0, 0);
		moveAction.target = moveAction.vector + base_pos;
		if (moveQueue.Count == 0 && char_direction * moveAction.vector.x < 0)
			Flip ();

		moveQueue.Add (moveAction);
		//transform.Translate (new Vector3 (blackbox.GetComponent<Collider2D>().bounds.size.x*direction, 0, 0));

	}

	void Jump(int direction)
	{	
//		if (direction != char_direction)
//			Flip ();
		MoveAction moveAction = new MoveAction("jump");
		moveAction.direction = direction*char_direction;
		moveQueue.Add (moveAction);
	}

	public void Restart(){
		Application.LoadLevel(Application.loadedLevel);
		Callback(System.Reflection.MethodBase.GetCurrentMethod().Name);
	}

	public void Lose(){
		Debug.Log ("Lose");
		Application.LoadLevel(Application.loadedLevel);
		Callback(System.Reflection.MethodBase.GetCurrentMethod().Name);
	}
	public void Win(){
		Debug.Log ("Win");
		Application.LoadLevel(Application.loadedLevel);
		Callback(System.Reflection.MethodBase.GetCurrentMethod().Name);
	}

	void setGravity(float gravity){
		Physics.gravity = new Vector3(0f, gravity, 0f);
		Callback(System.Reflection.MethodBase.GetCurrentMethod().Name);
	}

	void Flip(){
		char_direction *= -1;
		transform.Rotate(new Vector3(0, (transform.rotation.y == 180) ? 0 : 180, 0));
	}
	void Callback(string eventName){
		Application.ExternalCall( "callbackEvent", eventName);
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space))
			Jump (MoveAction.BACK);
		if (Input.GetKeyDown (KeyCode.UpArrow))
			Jump (MoveAction.FRONT);
		if (Input.GetKeyDown (KeyCode.RightArrow))
			Move (MoveAction.FRONT);
		if (Input.GetKeyDown (KeyCode.LeftArrow))
			Move (MoveAction.BACK);
		if (Input.GetKeyDown (KeyCode.F))
			Flip ();
		//anim.SetFloat ("Speed", Mathf.Abs(axis));
		if (moveQueue.Count > 0) {
			anim.SetFloat ("Speed", 0.9f);
			if (moveQueue[0].type == "jump"){
				if (rb.velocity.magnitude < 0.01 && !moveQueue[0].inAction){
					MoveAction ma = moveQueue[0];
					ma.inAction = true;
					moveQueue[0] = ma;
					rb.AddForce (new Vector2 (moveQueue[0].direction*3f, 6f), ForceMode2D.Impulse);
				} else if (rb.velocity.magnitude < 0.01) {
					if (moveQueue.Count > 1 && moveQueue[0].needFleep(moveQueue[1], char_direction)){
						Flip ();
					}
					moveQueue.RemoveAt (0);
					Callback("Jump");
				}
			} else {
				transform.position = Vector3.MoveTowards (transform.position, moveQueue[0].target, Time.deltaTime * 0.8f);

				if (Mathf.Abs (transform.position.x - moveQueue[0].target.x) < 0.3) {
					if (moveQueue.Count > 1 && moveQueue[0].needFleep(moveQueue[1], char_direction)){
						Flip ();
					}
					Callback("Move");
					moveQueue.RemoveAt (0);
				}
			}
		} else
			anim.SetFloat ("Speed", 0);
	}
	
	void OnCollisionEnter2D(Collision2D collision) {

		foreach (ContactPoint2D contact in collision.contacts) {
			if (contact.point.y < GetComponent<Collider2D>().bounds.min.y){
				if (moveQueue.Count > 0){
					moveQueue.RemoveAt (0);
					Callback("Collision");
				}
			}
		}

		
	}
}
