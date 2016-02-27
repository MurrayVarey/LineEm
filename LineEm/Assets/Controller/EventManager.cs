using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

	public delegate void TileClickAction(TileDisplay tile);
	public static event TileClickAction OnTileClicked;

	public static void OnTileClickedEvent(TileDisplay tile)
	{
		OnTileClicked(tile);
	}
}
