using UnityEngine;
using System.Collections;

public class CatchColorBall : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "ColoredSphere") {
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			player.GetComponent<SpringJoint2D>().enabled = false;

			GetComponent<SpringJoint2D>().enabled = true;
			GetComponent<SpringJoint2D>().connectedBody = other.GetComponent<Rigidbody2D>();


			GameObject.FindGameObjectWithTag("GameController").GetComponent<MainComponents>(
				).CChangerTarget.ApplyColors(other.GetComponent<LevelColorData>());

			this.enabled = false;
		}
	}

}
