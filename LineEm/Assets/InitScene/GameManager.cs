using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public enum eMove
	{
		PlayerOne,
		PlayerTwo
	}
	private eMove _currentMove;

	public enum eNextMoveOptions
	{
		Alternating,
		Random
	}

	private eNextMoveOptions _nextMoveOption;

	private int _playerCount;

	private static GameManager _instance;

	public static GameManager Instance()
	{
		if(_instance == null)
		{
			_instance = new GameObject().AddComponent<GameManager>();
		}
		return _instance;
	}

	void Awake()
	{
		_currentMove = eMove.PlayerOne;
		_nextMoveOption = eNextMoveOptions.Alternating;
		_playerCount = 2;
	}

	public eMove GetNextMove()
	{
		int nextMove;
		if(_nextMoveOption == eNextMoveOptions.Alternating)
		{
			nextMove = ((int)_currentMove+1)%2;
		}
		else
		{
			nextMove = Random.Range(0,2);
		}
		return (eMove)nextMove;
	}

	public void SetPlayerCount(int playerCount)
	{
		_playerCount = playerCount;
		print("Player Count: " + _playerCount);
	}

}
