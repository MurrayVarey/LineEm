using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReplayButtonActivator : MonoBehaviour {

	private Button _replayButton;
	private Text _replayText;

	void Awake()
	{
		EventManager.OnGameOver += ActivateReplayButton;
	}

	void OnDestroy()
	{
		EventManager.OnGameOver -= ActivateReplayButton;
	}

	void Start()
	{
		_replayButton = GameObject.Find("ReplayButton").GetComponent<Button>();
		_replayButton.interactable = false;

		_replayText = GameObject.Find("ReplayButtonText").GetComponent<Text>();
		_replayText.enabled = false;
		//Button buttonScript = _replayButton.GetComponent<Button>();
		//buttonScript.interactable = false;
	}

	public void ActivateReplayButton(int winner)
	{
		_replayButton.interactable = true;
		_replayText.enabled = true;
		//Button buttonScript = _replayButton.GetComponent<Button>();
		//buttonScript.interactable = true;
	}
}
