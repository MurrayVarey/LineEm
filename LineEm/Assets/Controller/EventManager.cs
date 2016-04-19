using UnityEngine;
using System.Collections;
using NoughtsAndCrosses;

public class EventManager : MonoBehaviour {

	public delegate void MoveMadeAction(Move move, eState moveState);
	public static event MoveMadeAction OnMoveMade;

	public static void OnMoveMadeEvent(Move move, eState moveState)
	{
		OnMoveMade(move, moveState);
	}

	public delegate void GameOverAction(int winningPlayer);
	public static event GameOverAction OnGameOver;

	public static void OnGameOverEvent(int winningPlayer)
	{
		OnGameOver(winningPlayer);
	}
}
