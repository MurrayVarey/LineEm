﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadOnClick : MonoBehaviour {

	public void LoadScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
}
