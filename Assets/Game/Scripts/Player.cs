using Assets.Game.Code;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public Direction Direction = Direction.None;
	public Direction QueuedDirection = Direction.None;
	public float Speed = 1f;
	public Point Position = new Point(-1, -1);

	private static Dictionary<TileType, bool> _WalkingAllowedOnTile = new Dictionary<TileType, bool>()
	{
		{ TileType.Floor, true },
		{ TileType.Gate, false },
		{ TileType.Teleporter, true },
		{ TileType.Wall, false },
	};

	void Start()
	{

	}

	void FixedUpdate()
	{
		if (Direction == Direction.None)
		{
			if (Position.x != 1 && Position.y != -1)
				MakeDecision(GameManager.gameManager.Grid[Position.x, Position.y]);
		}

		// Movement
		Vector3 movement = new Vector3();

		if (Direction == Direction.Up)
			movement = Vector3.up;
		else if (Direction == Direction.Down)
			movement = Vector3.down;
		else if (Direction == Direction.Left)
			movement = Vector3.left;
		else if (Direction == Direction.Right)
			movement = Vector3.right;

		movement *= Time.deltaTime;

		transform.position += movement * Speed;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Tile tile = other.GetComponent<Tile>();
		if (tile != null)
		{
			Position = tile.Position;

			if (tile.Pellet != null)
			{
				GetComponent<AudioSource>().Play();
				Destroy(tile.Pellet);
				tile.Pellet = null;
				GameManager.gameManager.Pellets--;
				GameManager.gameManager.UpdateText();
				if (GameManager.gameManager.Pellets == 0)
				{
					GameManager.gameManager.LoseGame();
				}
			}
			MakeDecision(tile);
		}

		Ghost ghost = other.GetComponent<Ghost>();
		if (ghost != null)
		{
			enabled = false;
			GameManager.gameManager.WinGame();
		}
	}

	private void MakeDecision(Tile tile)
	{
		Tile[,] grid = GameManager.gameManager.Grid;
		bool[,] gridSearch = new bool[grid.GetLength(0), grid.GetLength(1)];

		for (int x = 0; x < grid.GetLength(0); x++)
		{
			for (int y = 0; y < grid.GetLength(1); y++)
			{
				if (grid[x, y] != null)
				{
					gridSearch[x, y] = _WalkingAllowedOnTile[grid[x, y].Type];
				}
			}
		}

		Ghost ghost1 = GameManager.gameManager.Ghost1;
		if (ghost1.Position.x != -1 && ghost1.Position.y != -1)
		{
			gridSearch[ghost1.Position.x, ghost1.Position.y] = false;
			gridSearch[ghost1.Position.x, ghost1.Position.y - 1] = false;
			gridSearch[ghost1.Position.x, ghost1.Position.y + 1] = false;
			gridSearch[ghost1.Position.x - 1, ghost1.Position.y] = false;
			gridSearch[ghost1.Position.x + 1, ghost1.Position.y] = false;
			switch (ghost1.Direction)
			{
				case Direction.Up:
					gridSearch[ghost1.Position.x, ghost1.Position.y - 1] = false;
					gridSearch[ghost1.Position.x, ghost1.Position.y - 2] = false;
					gridSearch[ghost1.Position.x - 1, ghost1.Position.y - 1] = false;
					gridSearch[ghost1.Position.x + 1, ghost1.Position.y - 1] = false;
					break;
				case Direction.Down:
					gridSearch[ghost1.Position.x, ghost1.Position.y + 1] = false;
					gridSearch[ghost1.Position.x, ghost1.Position.y + 2] = false;
					gridSearch[ghost1.Position.x - 1, ghost1.Position.y + 1] = false;
					gridSearch[ghost1.Position.x + 1, ghost1.Position.y + 1] = false;
					break;
				case Direction.Left:
					gridSearch[ghost1.Position.x - 1, ghost1.Position.y] = false;
					gridSearch[ghost1.Position.x - 2, ghost1.Position.y] = false;
					gridSearch[ghost1.Position.x - 1, ghost1.Position.y - 1] = false;
					gridSearch[ghost1.Position.x - 1, ghost1.Position.y + 1] = false;
					break;
				case Direction.Right:
					gridSearch[ghost1.Position.x + 1, ghost1.Position.y] = false;
					gridSearch[ghost1.Position.x + 2, ghost1.Position.y] = false;
					gridSearch[ghost1.Position.x + 1, ghost1.Position.y - 1] = false;
					gridSearch[ghost1.Position.x + 1, ghost1.Position.y + 1] = false;
					break;
			}
		}
		Ghost ghost2 = GameManager.gameManager.Ghost2;

		if (ghost2.Position.x != -1 && ghost2.Position.y != -1)
		{
			gridSearch[ghost2.Position.x, ghost2.Position.y] = false;
			gridSearch[ghost2.Position.x, ghost2.Position.y - 1] = false;
			gridSearch[ghost2.Position.x, ghost2.Position.y + 1] = false;
			gridSearch[ghost2.Position.x - 1, ghost2.Position.y] = false;
			gridSearch[ghost2.Position.x + 1, ghost2.Position.y] = false;
			switch (ghost2.Direction)
			{
				case Direction.Up:
					gridSearch[ghost2.Position.x, ghost2.Position.y - 1] = false;
					gridSearch[ghost2.Position.x, ghost2.Position.y - 2] = false;
					gridSearch[ghost2.Position.x - 1, ghost2.Position.y - 1] = false;
					gridSearch[ghost2.Position.x + 1, ghost2.Position.y - 1] = false;
					break;
				case Direction.Down:
					gridSearch[ghost2.Position.x, ghost2.Position.y + 1] = false;
					gridSearch[ghost2.Position.x, ghost2.Position.y + 2] = false;
					gridSearch[ghost2.Position.x - 1, ghost2.Position.y + 1] = false;
					gridSearch[ghost2.Position.x + 1, ghost2.Position.y + 1] = false;
					break;
				case Direction.Left:
					gridSearch[ghost2.Position.x - 1, ghost2.Position.y] = false;
					gridSearch[ghost2.Position.x - 2, ghost2.Position.y] = false;
					gridSearch[ghost2.Position.x - 1, ghost2.Position.y - 1] = false;
					gridSearch[ghost2.Position.x - 1, ghost2.Position.y + 1] = false;
					break;
				case Direction.Right:
					gridSearch[ghost2.Position.x + 1, ghost2.Position.y] = false;
					gridSearch[ghost2.Position.x + 2, ghost2.Position.y] = false;
					gridSearch[ghost2.Position.x + 1, ghost2.Position.y - 1] = false;
					gridSearch[ghost2.Position.x + 1, ghost2.Position.y + 1] = false;
					break;
			}
		}

		List<Point> upPoints = new List<Point>();
		List<Point> downPoints = new List<Point>();
		List<Point> leftPoints = new List<Point>();
		List<Point> rightPoints = new List<Point>();

		bool advancedSearchNeeded = true;
		List<Direction> targetDirections = new List<Direction>();

		{
			int x = tile.Position.x;
			int y = tile.Position.y;

			if (gridSearch[x, y - 1])
			{
				if (grid[x, y - 1].Pellet != null)
				{
					advancedSearchNeeded = false;
					targetDirections.Add(Direction.Up);
				}
				upPoints.Add(new Point(x, y - 1));
				gridSearch[x, y - 1] = false;
			}

			if (gridSearch[x, y + 1])
			{
				if (grid[x, y + 1].Pellet != null)
				{
					advancedSearchNeeded = false;
					targetDirections.Add(Direction.Down);
				}
				downPoints.Add(new Point(x, y + 1));
				gridSearch[x, y + 1] = false;
			}

			if (gridSearch[x - 1, y])
			{
				if (grid[x - 1, y].Pellet != null)
				{
					advancedSearchNeeded = false;
					targetDirections.Add(Direction.Left);
				}
				leftPoints.Add(new Point(x - 1, y));
				gridSearch[x - 1, y] = false;
			}

			if (gridSearch[x + 1, y])
			{
				if (grid[x + 1, y].Pellet != null)
				{
					advancedSearchNeeded = false;
					targetDirections.Add(Direction.Right);
				}
				rightPoints.Add(new Point(x + 1, y));
				gridSearch[x + 1, y] = false;
			}
		}

		if (advancedSearchNeeded)
		{
			while (targetDirections.Count == 0)
			{
				if (upPoints.Count == 0 && downPoints.Count == 0 && leftPoints.Count == 0 && rightPoints.Count == 0)
				{
					break;
				}

				List<Point> newUpPoints = new List<Point>();
				foreach (Point pos in upPoints)
				{
					if (CheckNeighbourPositions(pos, newUpPoints, grid, gridSearch))
					{
						targetDirections.Add(Direction.Up);
						break;
					}
				}
				upPoints = newUpPoints;

				List<Point> newDownPoints = new List<Point>();
				foreach (Point pos in downPoints)
				{
					if (CheckNeighbourPositions(pos, newDownPoints, grid, gridSearch))
					{
						targetDirections.Add(Direction.Down);
						break;
					}
				}
				downPoints = newDownPoints;

				List<Point> newLeftPoints = new List<Point>();
				foreach (Point pos in leftPoints)
				{
					if (CheckNeighbourPositions(pos, newLeftPoints, grid, gridSearch))
					{
						targetDirections.Add(Direction.Left);
						break;
					}
				}
				leftPoints = newLeftPoints;

				List<Point> newRightPoints = new List<Point>();
				foreach (Point pos in rightPoints)
				{
					if (CheckNeighbourPositions(pos, newRightPoints, grid, gridSearch))
					{
						targetDirections.Add(Direction.Right);
						break;
					}
				}
				rightPoints = newRightPoints;
			}
		}

		Direction targetDirection = Direction.None;

		if (targetDirections.Count > 0)
		{
			targetDirection = targetDirections[Random.Range(0, targetDirections.Count)];
		}

		if (targetDirection != Direction)
		{
			Vector3 tempPos = tile.transform.position;
			tempPos.z = transform.position.z;
			transform.position = tempPos;
			Direction = targetDirection;
		}
	}

	bool CheckNeighbourPositions(Point point, List<Point> list, Tile[,] grid, bool[,] gridSearch)
	{
		int x = point.x;
		int y = point.y;

		if (gridSearch[x, y - 1])
		{
			if (grid[x, y - 1].Pellet != null)
			{
				return true;
			}
			list.Add(new Point(x, y - 1));
			gridSearch[x, y - 1] = false;
		}

		if (gridSearch[x, y + 1])
		{
			if (grid[x, y + 1].Pellet != null)
			{
				return true;
			}
			list.Add(new Point(x, y + 1));
			gridSearch[x, y + 1] = false;
		}

		if (gridSearch[x - 1, y])
		{
			if (grid[x - 1, y].Pellet != null)
			{
				return true;
			}
			list.Add(new Point(x - 1, y));
			gridSearch[x - 1, y] = false;
		}

		if (gridSearch[x + 1, y])
		{
			if (grid[x + 1, y].Pellet != null)
			{
				return true;
			}
			list.Add(new Point(x + 1, y));
			gridSearch[x + 1, y] = false;
		}

		return false;
	}
}
