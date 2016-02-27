using UnityEngine;
using UnityEngine.SceneManagement;
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
	
	public void UpdateGridData(TileDisplay tile)
	{
		print("Tile clicked: Column " + tile._column   + " Row " + tile._row);
		bool moveMade = _gridData.PlaceMove(tile._column, tile._row);
		if(moveMade)
		{
			tile.UpdateDisplay(_gridData._move);
			if(_gridData.IsWinningMove(tile._column, tile._row))
			{
				SceneManager.LoadScene("EndGame");
				//print ("Winner!");
			}
			else if(_gridData.IsStalemate())
			{
				SceneManager.LoadScene("EndGame");
				//print ("Draw");
			}
			else
			{
				_gridData.UpdateMove();
			}
		}
	}
}
