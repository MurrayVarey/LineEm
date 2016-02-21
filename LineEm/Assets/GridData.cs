using UnityEngine;
using System.Collections;

public class GridData 
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
	public eTileState _move;
	private eTileState[,] _tileStates;

	public GridData (int width, int height)
	{
		_width = width;
		_height = height;
		_tileStates = new eTileState[width, height];
		_move = eTileState.nought;
		_moveCount = 0;
	}

	public bool PlaceMove(int column, int row)
	{
		Debug.Assert(IsValidTile(column, row));
		if(IsValidTile(column, row) && IsEmptyTile(column, row))
		{
			_tileStates[column, row] = _move;
			_moveCount++;
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

	public void UpdateMove()
	{
		_move = _move == eTileState.nought ? eTileState.cross : eTileState.nought;
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

	public bool IsDraw()
	{
		return _moveCount == _width * _height;
	}

}


