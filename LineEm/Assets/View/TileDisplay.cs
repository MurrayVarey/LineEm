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
		//float uOffset = (float)column/3;
		//float yOffset = (float)row/3;
		//_renderer.material.SetTextureOffset("_MainTex", new Vector2(uOffset, yOffset));
	}

	private void SetMaterial(Material material)
	{
		_renderer.material = material;
		float uOffset = 0.67f-(float)_column/3;
		float vOffset = 0.67f-(float)_row/3;
		_renderer.material.SetTextureScale("_MainTex", new Vector2(0.333f,0.333f));
		_renderer.material.SetTextureOffset("_MainTex", new Vector2(uOffset, vOffset));
	}
}
