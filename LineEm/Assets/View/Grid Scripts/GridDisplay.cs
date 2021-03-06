﻿using UnityEngine;
using System.Collections;
using NoughtsAndCrosses;

public class GridDisplay : MonoBehaviour 
{

	public GameObject _tilePrefab;

	private int _width;
	private int _height;

	void Awake()
	{
		EventManager.OnMoveMade += UpdateDisplay;
	}

	void OnDestroy()
	{
		EventManager.OnMoveMade -= UpdateDisplay;
	}

	public void CreateTiles(GridData gridData)
	{
		_width = gridData.GetWidth();
		_height = gridData.GetHeight();
		for(int column=0; column<_width; ++column)
		{
			for(int row=0; row<_height; ++row)
			{
				Vector3 tilePosition = new Vector3(CalculateTilePosition(column, _height), CalculateTilePosition(row, _width), 0.5f);
				GameObject tile = Instantiate(_tilePrefab, tilePosition, Quaternion.Euler(-90, 0, 0)) as GameObject;
				TileDisplay tileDisplay = tile.GetComponent<TileDisplay>();
				tileDisplay.SetCoordinates(column, row);

				// Don't display grid lines for the last column/row, so
				// that we get that traditional noughts and crosses grid.
				tileDisplay.EnableRightLine(column<_width-1);
				tileDisplay.EnableTopLine(row<_height-1);
				eState state = gridData.GetTileState(new Move(column, row));
				tileDisplay.UpdateDisplay(state);
				tile.name = CreateTileName(column, row);
			}
		}
	}

	private float CalculateTilePosition(int tile, int maxTiles)
	{
		// The middle tile should be at (0, 0).
		// Offset by half (assumes max tile value is odd for now).
		return 0.15f * (tile-((maxTiles-1)/2));
	}

	private string CreateTileName(int column, int row)
	{
		return "tile_" + column + "_" + row;
	}

	public void UpdateDisplay(Move move, eState moveState)
	{
		TileDisplay tile = GetTileDisplay(move);
		tile.UpdateDisplay(moveState);
	}

	private TileDisplay GetTileDisplay(Move move)
	{
		string name = CreateTileName(move._column, move._row);
		GameObject tile = GameObject.Find(name);
		return tile.GetComponent<TileDisplay>();
	}

	public delegate void PostFlashRoutine();

	public IEnumerator FlashWinningTiles(LineDefinition winningLine, PostFlashRoutine postFlashRoutine)
	{
		for(int flashCount = 0; flashCount < 6; ++flashCount)
		{
			yield return new WaitForSeconds(0.15f);
			bool flashOn = flashCount%2 == 0;

			for(int tileCount = 0; tileCount < 3; ++tileCount)
			{
				Move tilePosition = winningLine.GetMove(tileCount);
				TileDisplay tileDisplay = GetTileDisplay(tilePosition);
				tileDisplay.SetWinningFlashMaterial(flashOn);
			}
		}
		postFlashRoutine();
	}
}
