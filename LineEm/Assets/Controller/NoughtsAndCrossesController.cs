using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using NoughtsAndCrosses;


public class NoughtsAndCrossesController : MonoBehaviour {

	public int _gridWidth = 3;
	public int _gridHeight = 3;
	private Grid _gridDisplay;

	private GridData _gridData;

	private GameManager _gameManager;

	void Awake()
	{
		EventManager.OnTileClicked += UpdateGridData;
	}

	void Start () 
	{
		_gridData = new GridData(_gridWidth, _gridHeight);

		_gridDisplay = GameObject.Find("Grid").GetComponent<Grid>();
		_gridDisplay.CreateTiles(_gridWidth, _gridHeight);

		_gameManager = GameManager.Instance();
	}

	void OnDestroy()
	{
		EventManager.OnTileClicked -= UpdateGridData;
	}
	
	public void UpdateGridData(TileDisplay tile)
	{
		Move move = new Move(tile._column, tile._row);
		MakeMove(move);
	}

	private void MakeMove(Move move)
	{
		eState moveState = GetPlayerState(_gameManager.GetTurn());
		bool moveMade = _gridData.PlaceMove(move, moveState);
		if(moveMade)
		{
			UpdateDisplay(move, moveState);
			_gameManager.UpdateTurn();
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

	private void UpdateDisplay(Move move, eState moveState)
	{
		TileDisplay tile = _gridDisplay.GetTileDisplay(move);
		tile.UpdateDisplay(moveState);
		tile.PlaySound();
	}

	public eState GetPlayerState(int player)
	{
		return player == 0 ? eState.nought : eState.cross;
	}
}
