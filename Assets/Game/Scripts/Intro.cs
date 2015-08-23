﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
	public Text TextField;

	string _text;
	int currentPos;
	bool soundSwitch;

	void Start()
	{
		_text = TextField.text;

		StartCoroutine("ShowText");
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			PlayerPrefs.SetInt("Difficulty", 1);
			Application.LoadLevel("Ingame");
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			Application.LoadLevel("Ingame");
			PlayerPrefs.SetInt("Difficulty", 2);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			Application.LoadLevel("Ingame");
			PlayerPrefs.SetInt("Difficulty", 3);
		}
	}

	IEnumerator ShowText()
	{
		for (int i = 0; i < _text.Length; i++)
		{
			TextField.text = _text.Substring(0, i);
			soundSwitch = !soundSwitch;
			if (soundSwitch)
			{
				GetComponent<AudioSource>().Play();
			}
			if (_text[i] == '\n')
			{
				yield return new WaitForSeconds(0.5f);
			}
			else
			{
				yield return new WaitForSeconds(0.005f);
			}
		}
	}
}
