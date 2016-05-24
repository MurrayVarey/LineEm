﻿using UnityEngine;
using System.Collections;
using NoughtsAndCrosses;

public class TileDisplay : MonoBehaviour 
{
	public Material _idleMaterial;
	public Material _activeMaterial;
	public Material _winningMaterial;

	public GameObject _stateDisplay;
	public TextMesh _stateText;

	public int _row;
	public int _column;

	public AudioClip _writingSound;

	private Renderer _renderer;

	private NoughtsAndCrossesController _controller;

	private bool _isWinningTile = false;

	void Awake ()
	{
		_row = -1;
		_column = -1;

		_renderer = GetComponent<Renderer> ();
		SetMaterial(_idleMaterial);

		_stateText = _stateDisplay.GetComponent<TextMesh>();
		_stateText.text = "";
	}

	void Start () 
	{
		_controller = GameObject.Find("_SceneController").GetComponent<NoughtsAndCrossesController>();
	}

	void OnMouseOver()
	{
		SetMaterial(_activeMaterial);
		if(Input.GetMouseButtonDown(0))
		{
			GameManager gameManager = GameManager.Instance();
			if(gameManager.IsPlayerControlledTurn())
			{
				_controller.OnTileClicked(this);
			}
		}
	}

	void OnMouseExit()
	{
		SetMaterial(_idleMaterial);
	}

	public void UpdateDisplay(eState state)
	{
		if(state == eState.empty)
		{
			return;
		}
		_stateText.text = state == eState.nought ? "O" : "X" ;
	}

	public void SetCoordinates(int column, int row)
	{
		_column = column;
		_row = row;
	}

	public void EnableRightLine(bool active)
	{
		transform.Find("RightLine").gameObject.SetActive(active);
	}

	public void EnableTopLine(bool active)
	{
		transform.Find("TopLine").gameObject.SetActive(active);
	}

	public void PlaySound()
	{
		AudioSource audio = GetComponent<AudioSource>();
		audio.clip = _writingSound;
		audio.Play();
	}

	public void SetWinningMaterial()
	{
		SetMaterial(_winningMaterial);
		_isWinningTile = true;
	}

	private void SetMaterial(Material material)
	{
		if(_renderer != null && !_isWinningTile)
		{
			_renderer.material = material;
		}
	}
}
