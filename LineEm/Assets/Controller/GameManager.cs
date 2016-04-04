using UnityEngine;
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

	List<IMoveInput> _moveInputs = new List<IMoveInput>();

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		_turn = 0;
		SetPlayerCount(2);
	}

	public void UpdateTurn()
	{
		_turn = (_turn+1)%2;
	}

	public int GetTurn()
	{
		return _turn;
	}

	public void SetPlayerCount(int playerCount)
	{
		_playerCount = playerCount;
		_moveInputs.Clear();
		_moveInputs.Add(new PlayerInput());
		if(playerCount == 1)
		{
			_moveInputs.Add(new CPUInput());
		}
		else
		{
			_moveInputs.Add(new PlayerInput());
		}
	}

	public IMoveInput GetCurrentInput()
	{
		return _moveInputs[_turn];
	}

}
