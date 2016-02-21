using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour 
{
	public Material _idleMaterial;
	public Material _activeMaterial;

	public GameObject _stateDisplay;

	public int _row;
	public int _column;

	private Renderer _renderer;

	void Awake ()
	{
		_row = -1;
		_column = -1;
	}

	void Start () 
	{
		_renderer = GetComponent<Renderer> ();
		_renderer.material = _idleMaterial;
	}

	void OnMouseOver()
	{
		_renderer.material = _activeMaterial;
		if(Input.GetMouseButtonDown(0))
		{
			EventManager.OnTileClickedEvent(this);
		}
	}

	void OnMouseExit()
	{
		_renderer.material = _idleMaterial;
	}
}
