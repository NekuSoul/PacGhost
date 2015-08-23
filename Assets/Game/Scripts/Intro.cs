using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
	public Text TextField;

	string _text;
	int currentPos;
	bool soundSwitch;
	bool displayAll;

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
			PlayerPrefs.SetInt("Difficulty", 2);
			Application.LoadLevel("Ingame");
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			PlayerPrefs.SetInt("Difficulty", 3);
			Application.LoadLevel("Ingame");
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
		if (Input.GetKeyDown(KeyCode.Return))
		{
			displayAll = true;
		}
	}

	IEnumerator ShowText()
	{
		for (int i = 0; i < _text.Length; i++)
		{
			if(displayAll)
			{
				TextField.text = _text;
				break;
			}
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
