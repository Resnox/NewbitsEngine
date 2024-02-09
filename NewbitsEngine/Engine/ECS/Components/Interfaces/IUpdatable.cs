using Microsoft.Xna.Framework;

namespace NewbitsEngine.Engine.ECS.Components;

public interface IUpdatable
{
	public void Update(GameTime time);
}
