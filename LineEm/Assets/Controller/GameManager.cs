using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	private static GameManager _instance;

	public static GameManager Instance()
	{
		if(_instance == null)
		{
			_instance = new GameObject().AddComponent<GameManager>();
		}
		return _instance;
	}

	private int _turn;

	private int _playerCount;
	private int[] _scores;

	List<bool> _playerControlled = new List<bool>();

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		_turn = 0;
		SetPlayerCount(1);
		_scores = new int[2];
		EventManager.OnGameOver += IncrementScore;
	}

	void OnDestroy()
	{
		EventManager.OnGameOver -= IncrementScore;
	}

	public void UpdateTurn()
	{
		_turn = (_turn+1)%2;
	}

	public int GetTurn()
	{
		return _turn;
	}

	public int GetPlayerCount()
	{
		return _playerCount;
	}

	public void SetPlayerCount(int playerCount)
	{
		_playerCount = playerCount;
		// Player controlled = true; CPU controlled = false
		_playerControlled.Clear();
		_playerControlled.Add(true);
		_playerControlled.Add(playerCount == 2);
		/*_moveInputs.Add(new PlayerInput());
		if(playerCount == 1)
		{
			_moveInputs.Add(new CPUInput());
		}
		else
		{
			_moveInputs.Add(new PlayerInput());
		}*/
	}

	public bool IsPlayerControlledTurn()
	{
		return _playerControlled[_turn];
	}

	public void IncrementScore(int winner, LineDefinition winningLine)
	{
		if(winner > -1)
		{
			++_scores[winner];
		}
	}

	public void RefreshScores()
	{
		Array.Clear(_scores, 0, 2);
	}

	public int GetScore(int player)
	{
		return _scores[player];
	}

	/*public IMoveInput GetCurrentInput()
	{
		return _moveInputs[_turn];
	}*/


}
