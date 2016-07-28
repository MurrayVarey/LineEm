using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NoughtsAndCrosses
{
	public class CPUMoveFinder
	{
		/***********************************************************************
		* A class to calculate the next CPU move, given the current grid data. *
		***********************************************************************/
		private GridData _gridData;
		private int _maxDepth;

		public CPUMoveFinder (GridData gridData, bool isBeatable)
		{
			_gridData = gridData;
			_maxDepth = isBeatable ? 4 : -1;
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
				int moveRating = MiniMax(move, _gridData.Copy(), gameManager.GetTurn(), 0);
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

		private int MiniMax(Move move, GridData gridData, int turn, int depth)
		{
			// The MiniMax method gives the best possible move for the current grid data.
			// This works by, effectively, playing each possible game. If that game results
			// in a CPU win, then return a positive score; if it results in a CPU loss,
			// then return a negative score. 
			// The CPU and Player alternate turns. The Player is aiming to get the lowest 
			// possible score (i.e. a CPU loss / a Player win). Therefore, when the lowest
			// score possible for the Player is positive, then a perfect-playing CPU will
			// eventually win, and that is the move it should take.

			// We limit the depth, in order to create a beatable CPU.

			// Odd turns are the CPU, and therefore looking for Max.
			// Even turns are player one.
			bool isCPUMove = (turn % 2) == 1;
			eState moveState = new StatePlayerConverter().GetPlayerState(turn);
			gridData.PlaceMove(move, moveState);
			if(gridData.HasWinner())
			{
				int winScore = 10 - depth;
				return isCPUMove ? winScore: -winScore;	
			}
			else if(gridData.IsStalemate() || depth == _maxDepth)
			{
				return 0;
			}
			else
			{
				isCPUMove = !isCPUMove;
				int overallRating = isCPUMove ? -20 : 20;

				List<Move> nextMoves = gridData.GetPossibleMoves();
				foreach(Move nextMove in nextMoves)
				{
					int moveRating = MiniMax(nextMove, gridData.Copy(), (turn + 1) % 2, depth + 1);
					if(isCPUMove && moveRating > overallRating)
					{
						overallRating = moveRating;
					}
					else if(!isCPUMove && moveRating < overallRating)
					{
						overallRating = moveRating;
					}
				}
				return overallRating;
			}
		} 
	}
}

