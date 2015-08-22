using UnityEngine;
//using System;
using Assets.Game.Code;

public class GameManager : MonoBehaviour
{
	public static GameManager gameManager = null;

	public GameObject Level;
	public Tile[,] Grid;

	public Tile Wall;
	public Tile Floor;
	public GameObject Pellet;
	public Player Player;

	public Ghost Ghost1;

	// Use this for initialization
	void Start()
	{
		gameManager = this;

		RenderSettings.ambientLight = Color.black;
		CreateLevel();
		SetPositions();
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void CreateLevel()
	{
		int width = 29;
		int height = 29;

		string[] rows = new string[height];
		rows[00] = "WWWWWWWWWWWWWWWWWWWWWWWWWWWWW";
		rows[01] = "WPPPPPPPPPPPPWWWPPPPPPPPPPPPW";
		rows[02] = "WPWWWWPWWWWWPWWWPWWWWWPWWWWPW";
		rows[03] = "WPWWWWPWWWWWPWWWPWWWWWPWWWWPW";
		rows[04] = "WPWWWWPWWWWWPWWWPWWWWWPWWWWPW";
		rows[05] = "WPPPPPPPPPPPPPPPPPPPPPPPPPPPW";
		rows[06] = "WPWWWWPWWPWWWWWWWWWPWWPWWWWPW";
		rows[07] = "WPWWWWPWWPWWWWWWWWWPWWPWWWWPW";
		rows[08] = "WPPPPPPWWPPPPWWWPPPPWWPPPPPPW";
		rows[09] = "WWWWWWPWWWWWFWWWFWWWWWPWWWWWW";
		rows[10] = "XXXXXWPWWWWWFWWWFWWWWWPWXXXXX";
		rows[11] = "XXXXXWPWWFFFFFFFFFFFWWPWXXXXX";
		rows[12] = "XXXXXWPWWFWWWWGWWWWFWWPWXXXXX";
		rows[13] = "WWWWWWPWWFWWWWFWWWWFWWPWWWWWW";
		rows[14] = "TFFFFFPFFFWWFFFFFWWFFFPFFFFFT";
		rows[15] = "WWWWWWPWWFWWWWWWWWWFWWPWWWWWW";
		rows[16] = "XXXXXWPWWFWWWWWWWWWFWWPWXXXXX";
		rows[17] = "XXXXXWPWWFFFFFFFFFFFWWPWXXXXX";
		rows[18] = "XXXXXWPWWWWWFWWWFWWWWWPWXXXXX";
		rows[19] = "WWWWWWPWWWWWFWWWFWWWWWPWWWWWW";
		rows[20] = "WPPPPPPWWPPPPWWWPPPPWWPPPPPPW";
		rows[21] = "WPWWWWPWWPWWWWWWWWWPWWPWWWWPW";
		rows[22] = "WPWWWWPWWPWWWWWWWWWPWWPWWWWPW";
		rows[23] = "WPPPPPPPPPPPPPFPPPPPPPPPPPPPW";
		rows[24] = "WPWWWWPWWWWWPWWWPWWWWWPWWWWPW";
		rows[25] = "WPWWWWPWWWWWPWWWPWWWWWPWWWWPW";
		rows[26] = "WPWWWWPWWWWWPWWWPWWWWWPWWWWPW";
		rows[27] = "WPPPPPPPPPPPPWWWPPPPPPPPPPPPW";
		rows[28] = "WWWWWWWWWWWWWWWWWWWWWWWWWWWWW";

		Grid = new Tile[width, height];

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				Tile newTile;
				switch (rows[y][x])
				{
					case 'W':
					case 'T':
						newTile = Instantiate(Wall);
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
		Player.transform.position = new Vector3(14, -23, -0.5f);
		Ghost1.transform.position = new Vector3(14, -11, -0.25f);
	}
}
