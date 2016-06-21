using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerCountDropdown : MonoBehaviour {

	private Dropdown _playerCountDropdown;
	private GameManager _gameManager;

	void Start()
	{
		_gameManager = GameManager.Instance();
		_playerCountDropdown = GameObject.Find("PlayerCountDropdown").GetComponent<Dropdown>();
		_playerCountDropdown.value = _gameManager.GetPlayerCount()-1;
	}

	public void SetPlayerCount(string sceneName)
	{
		int dropdownCount = _playerCountDropdown.value+1;
		if(dropdownCount != _gameManager.GetPlayerCount())
		{
			_gameManager.SetPlayerCount(dropdownCount);
			_gameManager.RefreshScores();
			SceneManager.LoadScene(sceneName);
		}
	}
}
