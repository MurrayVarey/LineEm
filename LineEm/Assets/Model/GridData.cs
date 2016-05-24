using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace NoughtsAndCrosses
{
	public enum eState
	{
		empty,
		nought,
		cross 
	}

	public class GridData
	{
		private int _width;
		private int _height;

		private eState[,] _tileStates;

		private LineDefinition _winningLine;

		public GridData(int width, int height)
		{
			_tileStates = null;
			_winningLine = null;
			_width = width;
			_height = height;
			_tileStates = new eState[width, height];
		}

		public GridData(eState[,] tileStates)
		{
			_tileStates = (eState[,])tileStates.Clone();
			_winningLine = null;
			_width = tileStates.GetLength(0);
			_height = tileStates.GetLength(1);
		}

		public int GetWidth() { return _width; }
		public int GetHeight() { return _height; }
		public eState GetTileState(Move move) { return _tileStates[move._column, move._row]; }

		public bool PlaceMove(Move move, eState moveState)
		{
			Debug.Assert(IsValidMove(move));
			if(IsValidMove(move) && IsEmptyTile(move))
			{
				_tileStates[move._column, move._row] = moveState;
				FindWinningLine(move);
				return true;
			}
			return false;
		}

		public GridData Copy()
		{
			return new GridData(_tileStates);
		}

		private bool IsValidMove(Move move)
		{
			return move._column < _width && move._row < _height;
		}

		private bool IsEmptyTile(Move move)
		{
			return _tileStates[move._column, move._row] == eState.empty;
		}

		private void FindWinningLine(Move move)
		{
			List<LineDefinition> lines = GetLines(move);
			foreach(LineDefinition line in lines)
			{
				if(IsWinningLine(line))
				{
					_winningLine = line;
				}
			}
		}

		private List<LineDefinition> GetLines(Move move)
		{
			List<LineDefinition> lines = new List<LineDefinition>();
			lines.Add(new RowDefinition(move._row));
			lines.Add(new ColumnDefinition(move._column));
			if(IsUpDiagonalTile(move))
			{
				lines.Add(new UpDiagonalDefinition());
			}
			if(IsDownDiagonalTile(move))
			{
				lines.Add(new DownDiagonalDefinition(_height - 1));
			}
			return lines;
		}

		private bool IsUpDiagonalTile(Move move)
		{
			return move._column == move._row;
		}

		private bool IsDownDiagonalTile(Move move)
		{
			return move._column == (_height - 1 - move._row);
		}

		private bool IsWinningLine(LineDefinition lineDefinition)
		{
			Move firstMove = lineDefinition.GetMove(0);
			eState firstState = _tileStates[firstMove._column, firstMove._row];

			if(firstState == eState.empty)
			{
				return false;
			}

			for(int tileCount = 1; tileCount < 3; ++tileCount)
			{
				Move move = lineDefinition.GetMove(tileCount);

				if(_tileStates[move._column, move._row] != firstState)
				{
					return false;
				}
			}
			return true;
		}

		public bool GameOver()
		{
			return HasWinner() || IsStalemate();
		}

		public bool HasWinner()
		{
			return _winningLine != null;
		}

		public LineDefinition GetWinningLine()
		{
			return _winningLine;
		}

		public bool IsStalemate()
		{
			for(int column = 0; column < _width; ++column)
			{
				for(int row = 0; row < _height; ++row)
				{
					if(_tileStates[column, row] == eState.empty)
					{
						return false;
					}
				}
			}
			return true;
		}

		public List<Move> GetPossibleMoves()
		{
			List<Move> moves = new List<Move>();
			for(int column = 0; column < _width; ++column)
			{
				for(int row = 0; row < _height; ++row)
				{
					if(_tileStates[column, row] == eState.empty)
					{
						moves.Add(new Move(column, row));
					}
				}
			}

			return moves;
		}

	}
}


