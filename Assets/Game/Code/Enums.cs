namespace Assets.Game.Code
{
	public enum Direction
	{
		None,
		Up,
		Down,
		Left,
		Right
	}

	public enum TileType
	{
		Floor,
		Wall,
		Gate,
		Teleporter
	}

	public enum GameState
	{
		PreGame,
		Ingame,
		Endgame
	}
}
