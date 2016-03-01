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
		SetMaterial(_idleMaterial);


		_stateText = _stateDisplay.GetComponent<TextMesh>();
		_stateText.text = "";
	}

	void OnMouseOver()
	{
		SetMaterial(_activeMaterial);
		if(Input.GetMouseButtonDown(0))
		{
			EventManager.OnTileClickedEvent(this);
		}
	}

	void OnMouseExit()
	{
		SetMaterial(_idleMaterial);
	}

	public void UpdateDisplay(GridData.eTileState state)
	{
		_stateText.text = state == GridData.eTileState.nought ? "O" : "X" ;
	}

	public void SetCoordinates(int column, int row)
	{
		_column = column;
		_row = row;
	}

	private void SetMaterial(Material material)
	{
		_renderer.material = material;
	}
}
