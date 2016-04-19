using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using NoughtsAndCrosses;


public class NoughtsAndCrossesController : MonoBehaviour {

	public int _gridWidth = 3;
	public int _gridHeight = 3;
	private Grid _gridDisplay;

	private GridData _gridData;

	private GameManager _gameManager;

	private bool _makingCPUMove = false;

	void Start () 
	{
		_gridData = new GridData(_gridWidth, _gridHeight);

		_gridDisplay = GameObject.Find("Grid").GetComponent<Grid>();
		_gridDisplay.CreateTiles(_gridWidth, _gridHeight);

		_gameManager = GameManager.Instance();
	}

	void Update()
	{
		if(ExpectingCPUMove())
		{
			StartCoroutine(MakeCPUMove());
		}
	}

	private bool ExpectingCPUMove()
	{
		return !_gameManager.IsPlayerControlledTurn() && !_gridData.GameOver() && !_makingCPUMove;
	}
	
	public void OnTileClicked(TileDisplay tile)
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
			EventManager.OnMoveMadeEvent(move, moveState);
			if(_gridData.HasWinner())
			{
				EventManager.OnGameOverEvent(_gameManager.GetTurn());
				//SceneManager.LoadScene("EndGame");
			}
			else if(_gridData.IsStalemate())
			{
				EventManager.OnGameOverEvent(-1);
				//SceneManager.LoadScene("EndGame");
			}
			_gameManager.UpdateTurn();
		}
	}

	public eState GetPlayerState(int player)
	{
		return player == 0 ? eState.nought : eState.cross;
	}

	private IEnumerator MakeCPUMove()
	{
		_makingCPUMove = true;
		yield return new WaitForSeconds(0.5f);
		List<Move> moves = _gridData.GetPossibleMoves();
		int iMove = Random.Range(0, moves.Count);
		MakeMove(moves[iMove]);
		_makingCPUMove = false;
	}
}
