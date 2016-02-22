using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

	public delegate void TileClickAction(TileScript tile);
	public static event TileClickAction OnTileClicked;

	public static void OnTileClickedEvent(TileScript tile)
	{
		OnTileClicked(tile);
	}
}
