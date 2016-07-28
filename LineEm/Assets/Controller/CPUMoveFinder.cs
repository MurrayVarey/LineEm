using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NoughtsAndCrosses
{
	public class CPUMoveFinder
	{
		private GridData _gridData;

		public CPUMoveFinder (GridData gridData)
		{
			_gridData = gridData;
		}

		public Move FindMove()
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
			GameManager gameManager = GameManager.Instance();

			int minRating = -20;
			List<Move> moves = _gridData.GetPossibleMoves();
			foreach(Move move in moves)
			{
				int maxDepth = gameManager.IsBeatable() ? 2 : -1;
				int moveRating = MiniMax(move, _gridData.Copy(), gameManager.GetTurn(), 0, maxDepth);
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

		private int MiniMax(Move move, GridData gridData, int turn, int depth, int maxDepth)
		{
			// Odd turns are the CPU, and therefore looking for Max.
			// Even turns are player one.
			bool isMax = (turn % 2) == 1;
			eState moveState = new StatePlayerConverter().GetPlayerState(turn);
			gridData.PlaceMove(move, moveState);
			if(gridData.HasWinner())
			{
				int winScore = 10 - depth;
				return isMax ? winScore: -winScore;	
			}
			else if(gridData.IsStalemate() || depth == maxDepth)
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
					int moveRating = MiniMax(nextMove, gridData.Copy(), (turn + 1) % 2, depth + 1, maxDepth);
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
}

