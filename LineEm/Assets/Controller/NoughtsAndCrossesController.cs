using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using NoughtsAndCrosses;


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
		_gridData = new GridData(_gridWidth, _gridHeight);

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
		Move move = new Move(tile._column, tile._row);
		eState moveState = GetPlayerState(manager.GetTurn());
		bool moveMade = _gridData.PlaceMove(move, moveState);
		if(moveMade)
		{
			tile.UpdateDisplay(moveState);
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

	public eState GetPlayerState(int player)
	{
		return player == 0 ? eState.nought : eState.cross;
	}
}
