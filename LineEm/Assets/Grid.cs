using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour 
{

	public GameObject _tilePrefab;

	public int _gridWidth = 3;
	public int _gridHeight = 3;

	void Start () 
	{
		CreateTiles();	
	}

	void CreateTiles()
	{
		for(int x=0; x<_gridWidth; ++x)
		{
			for(int z=0; z<_gridHeight; ++z)
			{
				Vector3 tilePosition = new Vector3(CalculateTilePosition(x, _gridWidth), 0, CalculateTilePosition(z, _gridHeight));
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
