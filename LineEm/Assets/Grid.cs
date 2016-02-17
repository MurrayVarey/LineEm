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
		for(int row=0; row<_width; ++row)
		{
			for(int column=0; column<_height; ++column)
			{
				Vector3 tilePosition = new Vector3(CalculateTilePosition(row, _width), 0, CalculateTilePosition(column, _height));
				GameObject tile = Instantiate(_tilePrefab, tilePosition, transform.rotation) as GameObject;
				TileScript tileScript = tile.GetComponent<TileScript>();
				tileScript._row = row;
				tileScript._column = column;
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
