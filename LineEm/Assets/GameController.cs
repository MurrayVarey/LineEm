using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public int _gridWidth = 3;
	public int _gridHeight = 3;
	private Grid _grid;

	private GridData _gridData;

	void Awake()
	{
		_gridData = new GridData(_gridWidth, _gridHeight);

		EventManager.OnTileClicked += UpdateGridData;
	}

	// Use this for initialization
	void Start () {
		_grid = GameObject.Find("Grid").GetComponent<Grid>();
		_grid.CreateTiles(_gridWidth, _gridHeight);
	}
	
	public void UpdateGridData(TileScript tile)
	{
		//print("Tile clicked: Row " + tile._row + " Column " + tile._column);
		_gridData.UpdateTile(tile._row, tile._column);
	}
}
