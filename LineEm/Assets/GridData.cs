using UnityEngine;
using System.Collections;

public class GridData : MonoBehaviour
{

	public enum eTileState
	{
		empty,
		nought,
		cross
	}

	private int _width;
	private int _height;

	public eTileState _nextMove;
	private eTileState[,] _tileStates;

	public GridData (int width, int height)
	{
		_width = width;
		_height = height;
		_tileStates = new eTileState[width, height];
		_nextMove = eTileState.nought;
	}

	public bool UpdateTile(int row, int column)
	{
		Debug.Assert(row < _width && column < _height);
		if(row >= _width || column >= _height)
		{
			return false;
		}
		else if(_tileStates[row, column] != eTileState.empty)
		{
			return false;
		}
		else
		{
			_tileStates[row, column] = _nextMove;
			_nextMove = GetNextMove(_nextMove);
			return true;
		}
	}

	private eTileState GetNextMove(eTileState currentMove)
	{
		return currentMove == eTileState.nought ? eTileState.cross : eTileState.nought;
	}
}


