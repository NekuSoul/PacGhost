using UnityEngine;
//using System;
using Assets.Game.Code;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static GameManager gameManager = null;

	public GameObject Level;
	public Tile[,] Grid;

	public Tile Wall;
	public Tile Floor;
	public Tile TeleporterLeft;
	public Tile TeleporterRight;
	public GameObject Pellet;
	public Player Player;

	public Ghost Ghost1;
	public Ghost Ghost2;

	public int Pellets = 0;

	public GameState State = GameState.PreGame;

	void Start()
	{
		gameManager = this;

		RenderSettings.ambientLight = Color.black;
		CreateLevel();
		SetPositions();

		StartCoroutine("StartGame");
	}

	IEnumerator StartGame()
	{
		yield return new WaitForSeconds(5);
		Player.enabled = true;
		Ghost1.enabled = true;
		Ghost2.enabled = true;
		State = GameState.Ingame;
	}

	public void WinGame()
	{
		Player.enabled = false;
		Player.gameObject.SetActive(false);
		Ghost1.enabled = false;
		Ghost2.enabled = false;
		State = GameState.Endgame;
	}

	public void LoseGame()
	{
		Player.enabled = false;
		Player.GetComponent<Animator>().Play("PlayerWin");
		Ghost1.enabled = false;
		Ghost1.GetComponent<Animator>().Play("GhostLose");
		Ghost2.enabled = false;
		Ghost2.GetComponent<Animator>().Play("GhostLose");
		State = GameState.Endgame;
	}

	void Update()
	{
		HandleInput();
	}

	private void HandleInput()
	{
		switch (State)
		{
			case GameState.Ingame:
				if (Input.GetKeyDown(KeyCode.W))
					Ghost1.QueuedDirection = Direction.Up;
				if (Input.GetKeyDown(KeyCode.S))
					Ghost1.QueuedDirection = Direction.Down;
				if (Input.GetKeyDown(KeyCode.A))
					Ghost1.QueuedDirection = Direction.Left;
				if (Input.GetKeyDown(KeyCode.D))
					Ghost1.QueuedDirection = Direction.Right;
				if (Input.GetKeyDown(KeyCode.I))
					Ghost2.QueuedDirection = Direction.Up;
				if (Input.GetKeyDown(KeyCode.K))
					Ghost2.QueuedDirection = Direction.Down;
				if (Input.GetKeyDown(KeyCode.J))
					Ghost2.QueuedDirection = Direction.Left;
				if (Input.GetKeyDown(KeyCode.L))
					Ghost2.QueuedDirection = Direction.Right;
				if (Input.GetKeyDown(KeyCode.R))
					Application.LoadLevel("Ingame");
				break;
		}
	}

	private void CreateLevel()
	{
		int width = 31;
		int height = 31;

		string[] rows = new string[height];
		rows[00] = "WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW";
		rows[01] = "WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW";
		rows[02] = "WWPPPPPPPPPPPPWWWPPPPPPPPPPPPWW";
		rows[03] = "WWPWWWWPWWWWWPWWWPWWWWWPWWWWPWW";
		rows[04] = "WWPWWWWPWWWWWPWWWPWWWWWPWWWWPWW";
		rows[05] = "WWPWWWWPWWWWWPWWWPWWWWWPWWWWPWW";
		rows[06] = "WWPPPPPPPPPPPPPPPPPPPPPPPPPPPWW";
		rows[07] = "WWPWWWWPWWPWWWWWWWWWPWWPWWWWPWW";
		rows[08] = "WWPWWWWPWWPWWWWWWWWWPWWPWWWWPWW";
		rows[09] = "WWPPPPPPWWPPPPWWWPPPPWWPPPPPPWW";
		rows[10] = "WWWWWWWPWWWWWFWWWFWWWWWPWWWWWWW";
		rows[11] = "XXXXXXWPWWWWWFWWWFWWWWWPWXXXXXX";
		rows[12] = "XXXXXXWPWWFFFFFFFFFFFWWPWXXXXXX";
		rows[13] = "XXXXXXWPWWFWWWWGWWWWFWWPWXXXXXX";
		rows[14] = "WWWWWWWPWWFWWWWFWWWWFWWPWWWWWWW";
		rows[15] = "WWAFFFFPFFFWWFFFFFWWFFFPFFFFBWW";
		rows[16] = "WWWWWWWPWWFWWWWWWWWWFWWPWWWWWWW";
		rows[17] = "XXXXXXWPWWFWWWWWWWWWFWWPWXXXXXX";
		rows[18] = "XXXXXXWPWWFFFFFFFFFFFWWPWXXXXXX";
		rows[19] = "XXXXXXWPWWWWWFWWWFWWWWWPWXXXXXX";
		rows[20] = "WWWWWWWPWWWWWFWWWFWWWWWPWWWWWWW";
		rows[21] = "WWPPPPPPWWPPPPWWWPPPPWWPPPPPPWW";
		rows[22] = "WWPWWWWPWWPWWWWWWWWWPWWPWWWWPWW";
		rows[23] = "WWPWWWWPWWPWWWWWWWWWPWWPWWWWPWW";
		rows[24] = "WWPPPPPPPPPPPPPFPPPPPPPPPPPPPWW";
		rows[25] = "WWPWWWWPWWWWWPWWWPWWWWWPWWWWPWW";
		rows[26] = "WWPWWWWPWWWWWPWWWPWWWWWPWWWWPWW";
		rows[27] = "WWPWWWWPWWWWWPWWWPWWWWWPWWWWPWW";
		rows[28] = "WWPPPPPPPPPPPPWWWPPPPPPPPPPPPWW";
		rows[29] = "WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW";
		rows[30] = "WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW";

		Grid = new Tile[width, height];

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				Tile newTile;
				switch (rows[y][x])
				{
					case 'W':
						newTile = Instantiate(Wall);
						break;
					case 'A':
						newTile = TeleporterLeft;
						break;
					case 'B':
						newTile = TeleporterRight;
						break;
					case 'F':
					case 'G':
						newTile = Instantiate(Floor);
						break;
					case 'P':
						newTile = Instantiate(Floor);
						GameObject pellet = Instantiate(Pellet);
						pellet.transform.parent = newTile.transform;
						pellet.transform.localPosition.Set(0, 0, -0.5f);
						newTile.Pellet = pellet;
						Pellets++;
						break;
					case 'X':
					default:
						newTile = null;
						break;
				}
				if (newTile != null)
				{
					Grid[x, y] = newTile;
					newTile.Position = new Point(x, y);
					newTile.transform.parent = Level.transform;
					newTile.transform.position = new Vector3(x, -y);
				}
			}
		}
	}

	private void SetPositions()
	{
		Player.transform.position = new Vector3(15, -24, -0.5f);
		Player.Position = new Point(15, 24);
		Ghost1.transform.position = new Vector3(13, -15, -0.75f);
		Ghost1.Position = new Point(13, 15);
		Ghost2.transform.position = new Vector3(17, -15, -0.75f);
		Ghost2.Position = new Point(17, 15);
	}
}
