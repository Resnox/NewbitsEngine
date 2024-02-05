using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NewbitsEngine.Engine.ECS.Components;

public class RotateComponent: Component, IUpdatable
{
	public RotateComponent(Entity entity, Texture2D texture2D) : base(entity)
	{
		Entity newEntity = entity.Scene.CreateEntity();
		newEntity.Transform.Position = Vector2.One;
		SpriteRenderer spriteRenderer = newEntity.AddComponent<SpriteRenderer>();
		spriteRenderer.Sprite = new Sprite(texture2D);
		Console.WriteLine(texture2D.Name);
	}
	
	public void Update(GameTime time)
	{
		Transform.RotationDegrees += 360 * (float) time.ElapsedGameTime.TotalSeconds;
	}
}
