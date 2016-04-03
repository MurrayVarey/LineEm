using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NoughtsAndCrossesController : MonoBehaviour {

	public int _gridWidth = 3;
	public int _gridHeight = 3;
	private Grid _gridDisplay;

	private GridData _gridData;

	void Awake()
	{
		EventManager.OnTileClicked += UpdateGridData;
	}

	void Start () 
	{
		_gridData = GameObject.Find("_SceneController").GetComponent<GridData>();
		_gridData.SetSize(_gridWidth, _gridHeight);

		_gridDisplay = GameObject.Find("Grid").GetComponent<Grid>();
		_gridDisplay.CreateTiles(_gridWidth, _gridHeight);
	}

	void OnDestroy()
	{
		EventManager.OnTileClicked -= UpdateGridData;
	}
	
	public void UpdateGridData(TileDisplay tile)
	{
		GameManager manager = GameManager.Instance();
		bool moveMade = _gridData.PlaceMove(tile._column, tile._row, manager.GetTurn());
		if(moveMade)
		{
			tile.UpdateDisplay(_gridData._move);
			tile.PlaySound();
			manager.UpdateTurn();
			if(_gridData.HasWinner())
			{
				SceneManager.LoadScene("EndGame");
			}
			else if(_gridData.IsStalemate())
			{
				SceneManager.LoadScene("EndGame");
			}
		}
	}
}
