using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

	void Awake()
	{
		EventManager.OnGameWon += DrawWinningLine;
	}

	void OnDestroy()
	{
		EventManager.OnGameWon -= DrawWinningLine;
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
		int currentTurn = _gameManager.GetTurn();
		eState moveState = GetPlayerState(currentTurn);
		bool moveMade = _gridData.PlaceMove(move, moveState);
		if(moveMade)
		{
			_gameManager.UpdateTurn();
			EventManager.OnMoveMadeEvent(move, moveState);
			if(_gridData.HasWinner())
			{
				EventManager.OnGameWonEvent(currentTurn, _gridData.GetWinningLine());
			}
			else if(_gridData.IsStalemate())
			{
				_gameManager.ResetScene();
			}
		}
	}

	public void DrawWinningLine(int winner, LineDefinition winningLine)
	{
		if(winningLine != null)
		{
			StartCoroutine(_gridDisplay.FlashWinningTiles(winningLine, _gameManager.ResetScene));
		}
	}

	public eState GetPlayerState(int player)
	{
		return player == 0 ? eState.nought : eState.cross;
	}

	private IEnumerator MakeCPUMove()
	{
		_makingCPUMove = true;
		Stopwatch watch = new Stopwatch();
		watch.Start();
		Move cpuMove = FindCPUMove();
		watch.Stop();
		// Add a little pause if necessary, so that the cpu move doesn't appear instantly
		float waitTime = (500f - watch.ElapsedMilliseconds) / 1000f;
		if(waitTime > 0f)
		{
			yield return new WaitForSeconds(waitTime);
		}
		MakeMove(cpuMove);
		_makingCPUMove = false;
	}

	private Move FindCPUMove()
	{
		// To make the CPU seem less predictable, we'll allow it to
		// randomly pick from any one of the best moves
		List<Move> bestMoves = GetBestMoves();
		int iMove = Random.Range(0, bestMoves.Count);
		return bestMoves[iMove];
	}

	private List<Move> GetBestMoves()
	{
		List<Move> bestMoves = new List<Move>();

		int minRating = -20;
		List<Move> moves = _gridData.GetPossibleMoves();
		foreach(Move move in moves)
		{
			int moveRating = MiniMax(move, _gridData.Copy(), _gameManager.GetTurn());
			if(moveRating > minRating)
			{
				bestMoves.Clear();
				bestMoves.Add(move);
				minRating = moveRating;
			}
			else if(moveRating == minRating)
			{
				bestMoves.Add(move);
			}
		}
		return bestMoves;
	}

	private int MiniMax(Move move, GridData gridData, int turn)
	{
		// Odd turns are the CPU, and therefore looking for Max.
		// Even turns are player one.
		bool isMax = (turn%2) == 1;
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
				int moveRating = MiniMax(nextMove, gridData.Copy(), (turn+1)%2);
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
