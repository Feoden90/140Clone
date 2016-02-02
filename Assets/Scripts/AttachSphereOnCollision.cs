using UnityEngine;
using System.Collections;

public class AttachSphereOnCollision : MonoBehaviour {

	public AudioSource CatchSound;

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.layer == LayerMask.NameToLayer("PlayerBody") && enabled) {
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			player.GetComponent<SpringJoint2D>().enabled = true;
			player.GetComponent<SpringJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
			Instantiate(CatchSound);
			this.enabled = false;
		}
	}

}
