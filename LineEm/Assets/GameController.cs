using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public int _gridWidth = 3;
	public int _gridHeight = 3;
	private Grid _grid;

	private GridData _gridData;

	// Use this for initialization
	void Start () {
		_grid = GameObject.Find("Grid").GetComponent<Grid>();
		_grid.CreateTiles(_gridWidth, _gridHeight);

		_gridData = new GridData(_gridWidth, _gridHeight);

		EventManager.OnTileClicked += UpdateGridData;
	}
	
	public void UpdateGridData(TileScript tile)
	{
		print("Tile clicked: Row " + tile._row + " Column " + tile._column);
	}
}
