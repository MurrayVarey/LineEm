using UnityEngine;
using System.Collections;
using System;


public class GridData : MonoBehaviour 
{
	private int _width;
	private int _height;
	private int _moveCount;

	public enum eTileState
	{
		empty,
		nought,
		cross 
	}
	private eTileState[,] _tileStates;
	public eTileState _move;

	private bool _hasWinner;

	void Awake ()
	{
		_tileStates = null;
		_move = eTileState.empty;
		_moveCount = 0;
		_hasWinner = false;
	}

	public void SetSize(int width, int height)
	{
		_width = width;
		_height = height;
		_tileStates = new eTileState[width, height];
	}

	public bool PlaceMove(int column, int row, int player)
	{
		Debug.Assert(IsValidTile(column, row));
		if(IsValidTile(column, row) && IsEmptyTile(column, row))
		{
			_move = GetMove(player);
			_tileStates[column, row] = _move;
			Debug.Log("Column: " + column + " Row: " + row + " Player: " + player);
			_moveCount++;
			_hasWinner = IsWinningMove(column, row);
			return true;
		}
		return false;
	}

	private bool IsValidTile(int column, int row)
	{
		return column < _width && row < _height;
	}

	private bool IsEmptyTile(int column, int row)
	{
		return _tileStates[column, row] == eTileState.empty;
	}

	public eTileState GetMove(int player)
	{
		return player == 0 ? eTileState.nought : eTileState.cross;
	}

	public bool IsWinningMove(int column, int row)
	{
		return IsWinningRow(row) || IsWinningColumn(column) || IsWinningUpDiagonal(column, row) || IsWinningDownDiagonal(column, row) ;
	}

	private bool IsWinningRow(int row)
	{
		return IsWinningLine(new RowDefinition(row));
	}

	private bool IsWinningColumn(int column)
	{
		return IsWinningLine(new ColumnDefinition(column));
	}

	private bool IsWinningUpDiagonal(int column, int row)
	{
		if(IsUpDiagonalTile(column, row))
		{
			return IsWinningLine(new UpDiagonalDefinition());
		}
		return false;
	}

	private bool IsUpDiagonalTile(int column, int row)
	{
		return column == row;
	}

	private bool IsWinningDownDiagonal(int column, int row)
	{
		if(IsDownDiagonalTile(column, row))
		{
			return IsWinningLine(new DownDiagonalDefinition(_height-1));
		}
		return false;
	}

	private bool IsDownDiagonalTile(int column, int row)
	{
		return column == (_height - 1 - row);
	}

	private bool IsWinningLine(LineDefinition lineDefinition)
	{
		for(int tileCount = 0; tileCount < 3; ++tileCount)
		{
			int row = lineDefinition.GetTileRow(tileCount);
			int column = lineDefinition.GetTileColumn(tileCount);

			Debug.Assert(IsValidTile(column, row));

			if(_tileStates[column, row] != _move)
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


