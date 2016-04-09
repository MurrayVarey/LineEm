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

	List<bool> _playerControlled = new List<bool>();

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		_turn = 0;
		SetPlayerCount(1);
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

	/*public IMoveInput GetCurrentInput()
	{
		return _moveInputs[_turn];
	}*/


}
