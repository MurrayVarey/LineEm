using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {

	public int player;
	// Use this for initialization
	void Start () 
	{
		GameManager gameManager = GameManager.Instance();
		UnityEngine.UI.Text displayText = GetComponent<UnityEngine.UI.Text>();
		displayText.text = gameManager.GetScore(player).ToString();
	}
}
