using Microsoft.Xna.Framework.Graphics;

namespace NewbitsEngine.Core;

public interface IRenderableComponent
{
    public void Draw(SpriteBatch spriteBatch);
}