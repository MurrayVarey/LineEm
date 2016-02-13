using UnityEngine;
using System.Collections;

public class GridTile : MonoBehaviour 
{
	public Material _idleMaterial;
	public Material _activeMaterial;

	private Renderer _renderer;

	void Start () 
	{
		_renderer = GetComponent<Renderer> ();
		_renderer.material = _idleMaterial;
	}

	void OnMouseEnter()
	{
		_renderer.material = _activeMaterial;
	}

	void OnMouseExit()
	{
		_renderer.material = _idleMaterial;
	}
}
