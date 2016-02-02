using UnityEngine;
using System.Collections;

public class ColorChanger : MonoBehaviour {

	void Start(){
		ApplyColors (GetComponent<LevelColorData> ());
	}
	

	public void ApplyColors(LevelColorData data){

		BackGroundGenerator BgGenerator = GameObject.FindGameObjectWithTag (
			"GameController").GetComponent<MainComponents> ().BGGenerator;
		BgGenerator.FirstColor = data.FirstBGColor;
		BgGenerator.SecondColor = data.SecondBGColor;
		BgGenerator.ApplyBGColor ();

		GameObject level = GameObject.FindGameObjectWithTag ("TileMap");
		foreach (var renderer in level.GetComponentsInChildren<Renderer>()) {
			renderer.material.color =data.LevelColor;
		}

		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		foreach (var renderer in player.GetComponentsInChildren<Renderer>()) {
			renderer.material.color =data.PlayerColor;
		}

	}
}
