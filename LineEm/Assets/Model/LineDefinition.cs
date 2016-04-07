using System;

public class LineDefinition
{
	private int _firstColumn;
	private int _columnStep;

	private int _firstRow;
	private int _rowStep;

	public LineDefinition (	int firstColumn,
							int columnStep,
							int firstRow,
							int rowStep)
	{
		_firstColumn = firstColumn;
		_columnStep = columnStep;

		_firstRow = firstRow;
		_rowStep = rowStep;
	}

	public Move GetMove(int tileIndex)
	{
		int column = _firstColumn + (tileIndex * _columnStep);
		int row = _firstRow + (tileIndex * _rowStep);
		return new Move(column, row);
	}
}

public class RowDefinition : LineDefinition
{
	public RowDefinition(int row) : base(0, 1, row, 0) {}
}

public class ColumnDefinition : LineDefinition
{
	public ColumnDefinition(int column) : base(column, 0, 0, 1) {}
}

public class UpDiagonalDefinition : LineDefinition
{
	public UpDiagonalDefinition() : base(0, 1, 0, 1) {}
}

public class DownDiagonalDefinition : LineDefinition
{
	public DownDiagonalDefinition(int topRow) : base(0, 1, topRow, -1) {}
}


