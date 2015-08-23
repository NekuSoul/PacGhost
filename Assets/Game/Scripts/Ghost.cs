using Assets.Game.Code;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
	public Point Position = new Point(-1, -1);
	public Direction Direction = Direction.None;
	public Direction QueuedDirection = Direction.None;
	public float Speed = 1f;

	private static Dictionary<TileType, bool> _WalkingAllowedOnTile = new Dictionary<TileType, bool>()
	{
		{ TileType.Floor, true },
		{ TileType.Gate, true },
		{ TileType.Teleporter, true },
		{ TileType.Wall, false },
	};

	void Start()
	{

	}

	void FixedUpdate()
	{
		if ((Direction == Direction.None) && Position.x != -1 && Position.y != -1)
			ProcessMovement();

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
			ProcessMovement();

			if (tile.gameObject == GameManager.gameManager.TeleporterLeft.gameObject && Direction == Direction.None)
			{
				Vector3 tempPos = GameManager.gameManager.TeleporterRight.transform.position;
				tile.GetComponent<AudioSource>().Play();
				tempPos.z = transform.position.z;
				transform.position = tempPos;
				Direction = Direction.Left;
			}
			if (tile.gameObject == GameManager.gameManager.TeleporterRight.gameObject && Direction == Direction.None)
			{
				Vector3 tempPos = GameManager.gameManager.TeleporterLeft.transform.position;
				tile.GetComponent<AudioSource>().Play();
				tempPos.z = transform.position.z;
				transform.position = tempPos;
				Direction = Direction.Right;
			}
		}
	}

	void ProcessMovement()
	{
		Direction targetDirection = Direction;
		Tile[,] grid = GameManager.gameManager.Grid;

		switch (Direction)
		{
			case Direction.Up:
				if (!_WalkingAllowedOnTile[grid[Position.x, Position.y - 1].Type])
				{
					targetDirection = Direction.None;
				}
				break;
			case Direction.Down:
				if (!_WalkingAllowedOnTile[grid[Position.x, Position.y + 1].Type])
				{
					targetDirection = Direction.None;
				}
				break;
			case Direction.Left:
				if (!_WalkingAllowedOnTile[grid[Position.x - 1, Position.y].Type])
				{
					targetDirection = Direction.None;
				}
				break;
			case Direction.Right:
				if (!_WalkingAllowedOnTile[grid[Position.x + 1, Position.y].Type])
				{
					targetDirection = Direction.None;
				}
				break;
		}

		if (QueuedDirection != Direction.None)
		{
			switch (QueuedDirection)
			{
				case Direction.Up:
					if (_WalkingAllowedOnTile[grid[Position.x, Position.y - 1].Type])
					{
						targetDirection = Direction.Up;
						QueuedDirection = Direction.None;
					}
					break;
				case Direction.Down:
					if (_WalkingAllowedOnTile[grid[Position.x, Position.y + 1].Type])
					{
						targetDirection = Direction.Down;
						QueuedDirection = Direction.None;
					}
					break;
				case Direction.Left:
					if (_WalkingAllowedOnTile[grid[Position.x - 1, Position.y].Type])
					{
						targetDirection = Direction.Left;
						QueuedDirection = Direction.None;
					}
					break;
				case Direction.Right:
					if (_WalkingAllowedOnTile[grid[Position.x + 1, Position.y].Type])
					{
						targetDirection = Direction.Right;
						QueuedDirection = Direction.None;
					}
					break;
			}
		}

		if (targetDirection != Direction)
		{
			Vector3 tempPos = GameManager.gameManager.Grid[Position.x, Position.y].transform.position;
			tempPos.z = transform.position.z;
			transform.position = tempPos;
			Direction = targetDirection;

			if(Direction==Direction.None)
			{
				GetComponent<AudioSource>().Play();
			}
		}
	}
}
