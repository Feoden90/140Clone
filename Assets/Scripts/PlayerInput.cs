using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

	public GroundCharacter character;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//if (Input.GetAxis ("Horizontal") > 0) {
			character.Move(Input.GetAxis ("Horizontal"));
		//}

		if (Input.GetButtonDown ("Jump")) {
			character.Jump();
		}

	}
}
