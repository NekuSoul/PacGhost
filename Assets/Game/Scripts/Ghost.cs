using Assets.Game.Code;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
	Point Position;
	public Direction Direction = Direction.Left;
	public float Speed = 1f;

	private static Dictionary<TileType, bool> _WalkingAllowedOnTile = new Dictionary<TileType, bool>()
	{
		{ TileType.Floor, true },
		{ TileType.Gate, false },
		{ TileType.Teleporter, false },
		{ TileType.Wall, false },
	};

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		
	}
}
