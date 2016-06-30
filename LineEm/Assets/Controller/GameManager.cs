using UnityEngine;
using UnityEngine.SceneManagement;
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

	public string _sceneName;

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		_turn = 0;
		SetPlayerCount(1);
		_scores = new int[2];
		EventManager.OnGameWon += IncrementScore;
	}

	void OnDestroy()
	{
		EventManager.OnGameWon -= IncrementScore;
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

	public void ResetScene()
	{
		SceneManager.LoadScene("NoughtsAndCrosses");
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
