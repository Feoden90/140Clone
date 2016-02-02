//#define STARTPOS startTransform.position;
//#define ENDPOS endTransform.position;

using UnityEngine;
using System.Collections;


public class AnimatedPlatform : MonoBehaviour {

	public Transform startTransform;
	public Transform endTransform;
	public float moveTime;
	public float waitTime;

	private Vector2 distance;
	private float timeCount;

	// Use this for initialization
	void Start () {
		distance = endTransform.position - startTransform.position;
		transform.position = startTransform.position;
		timeCount = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timeCount += moveTime;
		transform.position = Vector2.Lerp (startTransform.position, endTransform.position, timeCount/moveTime);
		if (timeCount > moveTime) {
		}
	}

	public Vector2 GetPlatformVelocity(){return Vector2.zero;}
}
