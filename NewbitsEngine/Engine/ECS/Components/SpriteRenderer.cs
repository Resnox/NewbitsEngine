using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NewbitsEngine.Engine.ECS.Components;

public class SpriteRenderer: Component, IRenderable
{
	public Sprite Sprite { get; set; }
	public Vector2 Origin { get; set; }
	public Color Color { get; set; }
	
	public SpriteRenderer(Entity entity) : base(entity)
	{
		Sprite = null;
		Color = Color.White;
		Origin = Vector2.Zero;
	}
	
	public void Render(SpriteBatch spriteBatch)
	{
		if (Sprite == null)
			return;
		
		spriteBatch.Draw(Sprite.texture, Transform.Position, Sprite.sourceRectangle, Color, Transform.Rotation, Origin * new Vector2(Sprite.texture.Width, Sprite.texture.Height), Transform.Scale, SpriteEffects.None, 0f);
	}
}
