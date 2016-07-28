using UnityEngine;
using System.Collections;
using NoughtsAndCrosses;

public class ChalkAudio : MonoBehaviour {

	public AudioClip _writingSound;

	void Awake()
	{
		EventManager.OnMoveMade += PlaySound;
	}

	void OnDestroy()
	{
		EventManager.OnMoveMade -= PlaySound;
	}

	public void PlaySound(Move move, eState moveState)
	{
		GameManager gameManager = GameManager.Instance();
		if(gameManager.IsSoundOn())
		{
			AudioSource audio = GetComponent<AudioSource>();
			audio.clip = _writingSound;
			audio.Play();
		}
	}

}
