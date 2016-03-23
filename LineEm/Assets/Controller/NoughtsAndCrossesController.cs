using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NoughtsAndCrossesController : MonoBehaviour {

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
		bool moveMade = _gridData.PlaceMove(tile._column, tile._row);
		if(moveMade)
		{
			tile.UpdateDisplay(_gridData._move);
			tile.PlaySound();
			if(_gridData.IsWinningMove(tile._column, tile._row))
			{
				SceneManager.LoadScene("EndGame");
			}
			else if(_gridData.IsStalemate())
			{
				SceneManager.LoadScene("EndGame");
			}
			else
			{
				_gridData.UpdateMove();
			}
		}
	}
}
