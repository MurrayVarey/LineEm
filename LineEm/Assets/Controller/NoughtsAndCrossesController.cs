using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using NoughtsAndCrosses;


public class NoughtsAndCrossesController : MonoBehaviour {

	/***************************************************************
	* Controls the specific noughts and crosses game being played. *
	***************************************************************/

	public int _gridWidth = 3;
	public int _gridHeight = 3;
	private GridDisplay _gridDisplay;

	private GridData _gridData;

	private GameManager _gameManager;

	private bool _makingCPUMove = false;

	void Start () 
	{
		_gameManager = GameManager.Instance();
		_gridData = new GridData(_gridWidth, _gridHeight);

		_gridDisplay = GameObject.Find("Grid").GetComponent<GridDisplay>();
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
		eState moveState = new StatePlayerConverter().GetPlayerState(currentTurn);
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


	private IEnumerator MakeCPUMove()
	{
		_makingCPUMove = true;
		Stopwatch watch = new Stopwatch();
		watch.Start();
		CPUMoveFinder moveFinder = new CPUMoveFinder(_gridData, _gameManager.IsBeatable());
		Move cpuMove = moveFinder.FindMove();
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
}
