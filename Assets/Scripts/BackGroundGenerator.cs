using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackGroundGenerator : MonoBehaviour {

	public int PowOf2Steps;
	public Vector2 RectangleGridSize;
	public GameObject Rectangle;
	public Color FirstColor;
	public Color SecondColor;
	public AudioSource _music;

	private GameObject[] _rectangles;
	private float[] _offsets;
	private Vector2 _screensize;
	private Camera _camera;
	private Vector2 _numberofrects;
	private Vector2 _rectoffset;

	// Use this for initialization
	void Start () {

		_camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
		float camsize = _camera.orthographicSize;
		_screensize = new Vector2 (2.0f* Screen.width / Screen.height * camsize,2* camsize);

		_numberofrects = new Vector2 (RectangleGridSize.x, RectangleGridSize.y + 1);

		_rectangles = new GameObject[(int)_numberofrects.x * (int)_numberofrects.y];
		//Debug.Log (_screensize);

		_rectoffset = new Vector2 (_screensize.x / RectangleGridSize.x, _screensize.y / RectangleGridSize.y);

		_offsets = GenerateRandomNoise (_rectangles.Length, 1);

		for (int i = 0; i < _numberofrects.y; i++){

			GameObject List = new GameObject();
			List.transform.SetParent(this.transform);
			Color rowcol = GetRowColor(i);

			for (int j = 0; j < _numberofrects.x; j++){
				int index = i*(int)_numberofrects.x + j;

				_rectangles[index] = Instantiate(Rectangle);
				_rectangles[index].transform.localScale = new Vector2(_rectoffset.x, _rectoffset.y * 4);

				_rectangles[index].GetComponentInChildren<SpriteRenderer>().color = rowcol;
				_rectangles[index].transform.parent = List.transform;
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		Random.seed = 0;

		float[] samples = new float[(int)Mathf.Pow (2.0f, PowOf2Steps)];
		_music.GetSpectrumData (samples, 0, FFTWindow.Rectangular);
		float value = Mathf.Max(samples);

		for (int i = 0; i < _numberofrects.y; i++) {
			for (int j = 0; j < _numberofrects.x; j++){

				int index = i*(int)_numberofrects.x + j;

				_rectangles[index].transform.position =
					new Vector2(_camera.transform.position.x - _screensize.x /2 +_rectoffset.x * (j + 0.5f),
					            _camera.transform.position.y - _screensize.y /2 +
					            _rectoffset.y * (i-2.0f + value*Random.Range(0.5f,1.0f) + _offsets[index]));
			}
		}
	
	}

	private Color GetRowColor(int rowindex){
		//Color FirstColor = FirstColorM.color;
		//Color SecondColor = SecondColorM.color;
		return Color.Lerp(FirstColor,SecondColor, rowindex /(_numberofrects.y - 1));
	}

	private float[] GenerateRandomNoise(int length, int naverages){
		float[] data = new float[length];
		for (int i = 0; i < length; i++) {
			data [i] = Random.Range (0.0f, 1.0f);
		}
		int step = 0;
		while (step < naverages) {
			for (int i = 0; i < length; i++) {
				data [i] = (data[i] + data[(i + 1) % length])/2;
			}
			step++;
		}
		return data;
	}

	public void ApplyBGColor(){
		for (int i = 0; i < _numberofrects.y; i++){
			Color rowcol = GetRowColor(i);
			for (int j = 0; j < _numberofrects.x; j++){
				int index = i*(int)_numberofrects.x + j;
				_rectangles[index].GetComponentInChildren<SpriteRenderer>().color = rowcol;
			}
		}

	}
}
