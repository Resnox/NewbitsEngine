using Microsoft.Xna.Framework.Graphics;

namespace NewbitsEngine.Engine.ECS.Components;

public interface IRenderable
{
	public void Render(SpriteBatch spriteBatch);
}
