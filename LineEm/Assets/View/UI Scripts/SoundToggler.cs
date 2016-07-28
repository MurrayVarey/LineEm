using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoundToggler: MonoBehaviour {

	UnityEngine.UI.Text _buttonText;

	void Awake()
	{
		_buttonText = GetComponentInChildren<UnityEngine.UI.Text>();
	}

	public void ToggleSound()
	{
		GameManager gameManager = GameManager.Instance();
		gameManager.ToggleSound();

		_buttonText.text = gameManager.IsSoundOn() ? "On" : "Off";
	}
}
