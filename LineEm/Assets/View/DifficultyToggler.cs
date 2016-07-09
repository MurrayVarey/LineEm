using UnityEngine;
using System.Collections;

public class DifficultyToggler : MonoBehaviour {

	UnityEngine.UI.Text _buttonText;
	GameManager _gameManager;

	void Awake()
	{
		_gameManager = GameManager.Instance();
		_buttonText = GetComponentInChildren<UnityEngine.UI.Text>();
		SetButtonText();
	}

	public void ToggleDifficulty()
	{
		_gameManager.ToggleDifficulty();
		SetButtonText();
	}

	private void SetButtonText()
	{
		_buttonText.text = _gameManager.IsBeatable() ? "Possible" : "Impossible";	
	}
}
