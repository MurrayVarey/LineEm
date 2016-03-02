using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour 
{

	public GameObject _tilePrefab;

	private int _width;
	private int _height;

	void Start () 
	{
	}

	public void CreateTiles(int width, int height)
	{
		_width = width;
		_height = height;
		for(int column=0; column<_width; ++column)
		{
			for(int row=0; row<_height; ++row)
			{
				Vector3 tilePosition = new Vector3(CalculateTilePosition(column, _height), 0, CalculateTilePosition(row, _width));
				GameObject tile = Instantiate(_tilePrefab, tilePosition, transform.rotation) as GameObject;
				TileDisplay tileDisplay = tile.GetComponent<TileDisplay>();
				tileDisplay.SetCoordinates(column, row);
				// Don't display grid lines for the last column/row, so
				// that we get that traditional noughts and crosses grid.
				tileDisplay.EnableRightLine(column<_width-1);
				tileDisplay.EnableTopLine(row<_height-1);
			}
		}
	}

	private int CalculateTilePosition(int tile, int maxTiles)
	{
		// The middle tile should be at (0, 0).
		// Offset by half (assumes max tile value is odd for now).
		return tile-((maxTiles-1)/2);
	}
	
	void Update () 
	{
	
	}
}
