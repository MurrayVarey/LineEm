using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour 
{
	public Material _idleMaterial;
	public Material _activeMaterial;

	public int _row;
	public int _column;

	private Renderer _renderer;

	void Start () 
	{
		_renderer = GetComponent<Renderer> ();
		_renderer.material = _idleMaterial;
		_row = -1;
		_column = -1;
	}

	void OnMouseOver()
	{
		_renderer.material = _activeMaterial;
		/*if(Input.GetMouseButtonDown(0))
		{
			bool here = true;
		}*/
	}

	void OnMouseExit()
	{
		_renderer.material = _idleMaterial;
	}
}
