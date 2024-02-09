using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NewbitsEngine.Engine;

public class Sprite
{
	public Rectangle? sourceRectangle;
	public Texture2D texture;

	public Sprite(Texture2D texture, Rectangle? sourceRectangle = null)
	{
		this.texture = texture;
		this.sourceRectangle = sourceRectangle;
	}
}
