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

	public delegate void GameWonAction(int winningPlayer, LineDefinition winningLine);
	public static event GameWonAction OnGameWon;

	public static void OnGameWonEvent(int winningPlayer, LineDefinition winningLine)
	{
		OnGameWon(winningPlayer, winningLine);
	}
}
