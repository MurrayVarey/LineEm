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
		_gameManager = GameManager.Instance();
		_gridData = new GridData(_gridWidth, _gridHeight);

		_gridDisplay = GameObject.Find("Grid").GetComponent<Grid>();
		_gridDisplay.CreateTiles(_gridData);
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
			}
			else if(_gridData.IsStalemate())
			{
				EventManager.OnGameOverEvent(-1);
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
		MakeMove(FindCPUMove());
		_makingCPUMove = false;
	}

	private Move FindCPUMove()
	{
		Move cpuMove = null;
		int minRating = -20;

		List<Move> moves = _gridData.GetPossibleMoves();
		foreach(Move move in moves)
		{
			int moveRating = MiniMax(move, _gridData.Copy(), _gameManager.GetTurn(), true);
			if(moveRating > minRating)
			{
				cpuMove = move;
				minRating = moveRating;
			}
		}
		return cpuMove;
	}

	private int MiniMax(Move move, GridData gridData, int turn, bool isMax)
	{
		eState moveState = GetPlayerState(turn);
		bool moveMade = gridData.PlaceMove(move, moveState);

		if(gridData.HasWinner())
		{
			return isMax ? 10 : -10;	
		}
		else if(gridData.IsStalemate())
		{
			return 0;
		}
		else
		{
			isMax = !isMax;
			int overallRating = isMax ? -20 : 20;

			List<Move> nextMoves = gridData.GetPossibleMoves();
			foreach(Move nextMove in nextMoves)
			{
				int moveRating = MiniMax(nextMove, gridData.Copy(), (turn+1)%2, isMax);
				if(isMax && moveRating > overallRating)
				{
					overallRating = moveRating;
				}
				else if(!isMax && moveRating < overallRating)
				{
					overallRating = moveRating;
				}
			}
			return overallRating;
		}
	} 
}
