using UnityEngine;
using System.Collections;

public class TileDisplay : MonoBehaviour 
{
	public Material _idleMaterial;
	public Material _activeMaterial;

	public GameObject _stateDisplay;
	public TextMesh _stateText;

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

		_stateText = _stateDisplay.GetComponent<TextMesh>();
		_stateText.text = "";
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

	public void UpdateDisplay(GridData.eTileState state)
	{
		_stateText.text = state == GridData.eTileState.nought ? "O" : "X" ;
	}
}
