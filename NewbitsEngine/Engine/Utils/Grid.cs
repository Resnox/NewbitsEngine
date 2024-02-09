using Microsoft.Xna.Framework;

namespace NewbitsEngine.Engine.ECS.Components;

public class Grid<T>
{
	private readonly T[] grid;

	public Grid(int width, int height, T defaultValue = default(T))
	{
		Width = width;
		Height = height;

		grid = new T[width * height];
		for (int i = 0; i < grid.Length; i++)
		{
			grid[i] = defaultValue;
		}
	}
	public int Width
	{
		get;
	}
	public int Height { get; private set; }

	public T this[int x, int y]
	{
		get
		{
			return grid[x + (y * Width)];
		}
	}

	public T this[Point point]
	{
		get
		{
			return this[point.X, point.Y];
		}
	}
}
