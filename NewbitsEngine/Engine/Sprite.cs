using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NewbitsEngine.Engine;

public class Sprite
{
	public Texture2D texture;
	public Rectangle? sourceRectangle;

	public Sprite(Texture2D texture, Rectangle? sourceRectangle = null)
	{
		this.texture = texture;
		this.sourceRectangle = sourceRectangle;
	}
}
