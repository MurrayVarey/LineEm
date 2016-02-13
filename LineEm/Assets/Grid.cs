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
		for(int x=0; x<_width; ++x)
		{
			for(int z=0; z<_height; ++z)
			{
				Vector3 tilePosition = new Vector3(CalculateTilePosition(x, _width), 0, CalculateTilePosition(z, _height));
				Instantiate(_tilePrefab, tilePosition, transform.rotation);
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
