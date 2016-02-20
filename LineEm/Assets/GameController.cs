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

	void Start () 
	{
		_grid = GameObject.Find("Grid").GetComponent<Grid>();
		_grid.CreateTiles(_gridWidth, _gridHeight);
	}
	
	public void UpdateGridData(TileScript tile)
	{
		print("Tile clicked: Column " + tile._column   + " Row " + tile._row);
		bool moveMade = _gridData.PlaceMove(tile._column, tile._row);
		if(moveMade)
		{
			bool winner = _gridData.IsWinningMove(tile._column, tile._row);
			if(winner)
			{
				print ("Winner!");
			}
			else
			{
				_gridData.UpdateMove();
			}
		}
	}
}
