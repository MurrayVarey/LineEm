using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerToggler : MonoBehaviour {

	UnityEngine.UI.Text _buttonText;
	GameManager _gameManager;

	void Awake()
	{
		_gameManager = GameManager.Instance();
		_buttonText = GetComponentInChildren<UnityEngine.UI.Text>();
		_buttonText.text = _gameManager.GetPlayerCount() == 1 ? "1" : "2";	
	}

	public void TogglePlayers(string sceneName)
	{
		_gameManager.TogglePlayerCount();
		_gameManager.RefreshScores();
		SceneManager.LoadScene(sceneName);
	}
}
