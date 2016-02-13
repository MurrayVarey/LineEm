using System;


public class GridData
{

	enum eTileState
	{
		empty,
		nought,
		cross
	}

	private int _width;
	private int _height;

	private eTileState[,] _tileStates;

	public GridData (int width, int height)
	{
		_width = width;
		_height = height;
		_tileStates = new eTileState[width, height];
	}
}


