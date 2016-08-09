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

			int turn = 1;
			int minRating = -20;
			List<Move> moves = _gridData.GetPossibleMoves();
			foreach(Move move in moves)
			{
				int moveRating = MiniMax(move, _gridData.Copy(), turn, 0);
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

		private bool IsCPUMove(int turn)
		{
			// Odd turns are the CPU; Even turns are Player 1.
			return (turn % 2) == 1;
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

			eState moveState = new StatePlayerConverter().GetPlayerState(turn);
			gridData.PlaceMove(move, moveState);
			if(gridData.HasWinner())
			{
				int winScore = 10 - depth;
				return IsCPUMove(turn) ? winScore: -winScore;	
			}
			else if(gridData.IsStalemate() || depth == _maxDepth)
			{
				return 0;
			}
			else
			{
				// Game is neither won nor a stalemate. Therefore, we'll keep playing.
				int nextTurn = (turn + 1) % 2;
				int bestScore = -20;

				List<Move> nextMoves = gridData.GetPossibleMoves();
				foreach(Move nextMove in nextMoves)
				{
					int score = MiniMax(nextMove, gridData.Copy(), nextTurn, depth + 1);
					// Player move will return a negative score if it has won. Negate it for
					// now so that it is positive and will be seen as the player's best move.
					score = IsCPUMove(nextTurn) ? score : -score;
					if(score > bestScore)
					{
						bestScore = score;
					} 
				}
				// Player move has negated to find the best score. Turn it back, so that it
				// gives an accurate score for CPU move.
				return IsCPUMove(nextTurn) ? bestScore: -bestScore;
			}
		} 
	}
}

