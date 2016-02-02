using UnityEngine;
using System.Collections;

public class CameraAutoSet : MonoBehaviour {

	public float VisibleVerticalTiles = 10;

	private float _zposition;
	private Vector2 _mapunitysize;
	private Transform _player;
	private Vector2 _mapposition;

	// Use this for initialization
	void Start () {
		GameObject mapobj = GameObject.FindWithTag ("TileMap");
		Tiled2Unity.TiledMap map = mapobj.GetComponent<Tiled2Unity.TiledMap> ();

		_mapunitysize = new Vector2 (map.GetMapWidthInPixelsScaled (), map.GetMapHeightInPixelsScaled ());
		_mapposition = mapobj.transform.position;

		GetComponent<Camera> ().orthographicSize = VisibleVerticalTiles * map.TileHeight
			* map.ExportScale * mapobj.transform.localScale.y / 2;

		_zposition = transform.position.z;
	}

	// Update is called once per frame
	void Update () {
		if (_player == null) {
			GameObject playerobj = GameObject.FindWithTag ("Player");
			if(playerobj == null) return;
			_player = playerobj.transform;
		}
		float camsize = GetComponent<Camera> ().orthographicSize;
		float ratio = Screen.width / (float)Screen.height;
		
		Vector2 screensize = new Vector2 (camsize * ratio, camsize);
		Vector3 pos = _player.position;
		float mapx = Mathf.Clamp (pos.x, screensize.x, _mapunitysize.x -screensize.x);
		float mapy = Mathf.Clamp (pos.y, - _mapunitysize.y + screensize.y, -screensize.y);
		this.transform.position = new Vector3 (mapx + _mapposition.x, mapy + _mapposition.y, _zposition);
	
	}
}
