using UnityEngine;
using System.Collections;

public class TintSphere : MonoBehaviour {

	public Renderer Sphere1Renderer;
	public Renderer Sphere2Renderer;

	// Use this for initialization
	void Start () {
		LevelColorData data = GetComponent<LevelColorData> ();
		Sphere1Renderer.material.color = data.FirstBGColor;
		Sphere2Renderer.material.color = data.LevelColor;
	}

}
