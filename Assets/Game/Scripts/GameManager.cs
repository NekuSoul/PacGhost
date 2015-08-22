using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public GameObject Level;
    public GameObject[,] Grid;

    public GameObject Wall;
    public GameObject Floor;
    public GameObject Pellet;
    public GameObject Player;

    // Use this for initialization
    void Start()
    {
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
        rows[00] = "WWWWWWWWWWWWWWWWWWWWWWWWWWWWW" + Environment.NewLine;
        rows[01] = "WPPPPPPPPPPPPWWWPPPPPPPPPPPPW" + Environment.NewLine;
        rows[02] = "WPWWWWPWWWWWPWWWPWWWWWPWWWWPW" + Environment.NewLine;
        rows[03] = "WPWWWWPWWWWWPWWWPWWWWWPWWWWPW" + Environment.NewLine;
        rows[04] = "WPWWWWPWWWWWPWWWPWWWWWPWWWWPW" + Environment.NewLine;
        rows[05] = "WPPPPPPPPPPPPPPPPPPPPPPPPPPPW" + Environment.NewLine;
        rows[06] = "WPWWWWPWWPWWWWWWWWWPWWPWWWWPW" + Environment.NewLine;
        rows[07] = "WPWWWWPWWPWWWWWWWWWPWWPWWWWPW" + Environment.NewLine;
        rows[08] = "WPPPPPPWWPPPPWWWPPPPWWPPPPPPW" + Environment.NewLine;
        rows[09] = "WWWWWWPWWWWWFWWWFWWWWWPWWWWWW" + Environment.NewLine;
        rows[10] = "XXXXXWPWWWWWFWWWFWWWWWPWXXXXX" + Environment.NewLine;
        rows[11] = "XXXXXWPWWFFFFFFFFFFFWWPWXXXXX" + Environment.NewLine;
        rows[12] = "XXXXXWPWWFWWWWGWWWWFWWPWXXXXX" + Environment.NewLine;
        rows[13] = "WWWWWWPWWFWFFFFFFFWFWWPWWWWWW" + Environment.NewLine;
        rows[14] = "TFFFFFPFFFWFFFFFFFWFFFPFFFFFT" + Environment.NewLine;
        rows[15] = "WWWWWWPWWFWFFFFFFFWFWWPWWWWWW" + Environment.NewLine;
        rows[16] = "XXXXXWPWWFWWWWWWWWWFWWPWXXXXX" + Environment.NewLine;
        rows[17] = "XXXXXWPWWFFFFFFFFFFFWWPWXXXXX" + Environment.NewLine;
        rows[18] = "XXXXXWPWWWWWFWWWFWWWWWPWXXXXX" + Environment.NewLine;
        rows[19] = "WWWWWWPWWWWWFWWWFWWWWWPWWWWWW" + Environment.NewLine;
        rows[20] = "WPPPPPPWWPPPPWWWPPPPWWPPPPPPW" + Environment.NewLine;
        rows[21] = "WPWWWWPWWPWWWWWWWWWPWWPWWWWPW" + Environment.NewLine;
        rows[22] = "WPWWWWPWWPWWWWWWWWWPWWPWWWWPW" + Environment.NewLine;
        rows[23] = "WPPPPPPPPPPPPPFPPPPPPPPPPPPPW" + Environment.NewLine;
        rows[24] = "WPWWWWPWWWWWPWWWPWWWWWPWWWWPW" + Environment.NewLine;
        rows[25] = "WPWWWWPWWWWWPWWWPWWWWWPWWWWPW" + Environment.NewLine;
        rows[26] = "WPWWWWPWWWWWPWWWPWWWWWPWWWWPW" + Environment.NewLine;
        rows[27] = "WPPPPPPPPPPPPWWWPPPPPPPPPPPPW" + Environment.NewLine;
        rows[28] = "WWWWWWWWWWWWWWWWWWWWWWWWWWWWW" + Environment.NewLine;

        Grid = new GameObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject newTile;
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
                        newTile = Instantiate(Pellet);
                        break;
                    case 'X':
                    default:
                        newTile = null;
                        break;
                }
                if (newTile != null)
                {
                    Grid[x, y] = newTile;
                    newTile.transform.parent = Level.transform;
                    newTile.transform.position = new Vector3(x, y);
                }
            }
        }
    }

    private void SetPositions()
    {
        Player.transform.position = new Vector3(14, 23);
    }
}
