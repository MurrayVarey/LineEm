using UnityEngine;
using System.Collections;
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
		private int _moveCount;


		private eState[,] _tileStates;
		//public eTileState _move;

		private bool _hasWinner;

		public GridData(int width, int height)
		{
			_tileStates = null;
			//_move = eTileState.empty;
			_moveCount = 0;
			_hasWinner = false;
			_width = width;
			_height = height;
			_tileStates = new eState[width, height];
		}

		public bool PlaceMove(Move move, eState moveState)
		{
			Debug.Assert(IsValidMove(move));
			if(IsEmptyTile(move))
			{
				//_move = 
				_tileStates[move._column, move._row] = moveState;
				_moveCount++;
				_hasWinner = IsWinningMove(move);
				return true;
			}
			return false;
		}

		private bool IsValidMove(Move move)
		{
			return move._column < _width && move._row < _height;
		}

		private bool IsEmptyTile(Move move)
		{
			return _tileStates[move._column, move._row] == eState.empty;
		}

		public bool IsWinningMove(Move move)
		{
			return IsWinningRow(move._row) || IsWinningColumn(move._column) || IsWinningUpDiagonal(move) || IsWinningDownDiagonal(move) ;
		}

		private bool IsWinningRow(int row)
		{
			return IsWinningLine(new RowDefinition(row));
		}

		private bool IsWinningColumn(int column)
		{
			return IsWinningLine(new ColumnDefinition(column));
		}

		private bool IsWinningUpDiagonal(Move move)
		{
			if(IsUpDiagonalTile(move))
			{
				return IsWinningLine(new UpDiagonalDefinition());
			}
			return false;
		}

		private bool IsUpDiagonalTile(Move move)
		{
			return move._column == move._row;
		}

		private bool IsWinningDownDiagonal(Move move)
		{
			if(IsDownDiagonalTile(move))
			{
				return IsWinningLine(new DownDiagonalDefinition(_height-1));
			}
			return false;
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

		public bool HasWinner()
		{
			return _hasWinner;
		}

		public bool IsStalemate()
		{
			return _moveCount == _width * _height;
		}

	}
}


