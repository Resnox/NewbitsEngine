using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using NewbitsEngine.Engine.ECS.Components;

namespace NewbitsEngine.Engine.ECS;

public sealed class Scene
{
	public GraphicsDevice GraphicsDevice { get; }
	public EntityFactory EntityFactory { get; }
	
	private readonly List<Entity> entities;

	public Scene(GraphicsDevice graphicsDevice, EntityFactory entityFactory, Texture2D texture)
	{
		GraphicsDevice = graphicsDevice;
		EntityFactory = entityFactory;
		
		entities = new List<Entity>();

		Entity cameraEntity = CreateEntity();
		Camera = cameraEntity.AddComponent<Camera>();

		Entity testEntity = CreateEntity();
		SpriteRenderer spriteRenderer = testEntity.AddComponent<SpriteRenderer>();
		spriteRenderer.Sprite = new Sprite(texture);
		spriteRenderer.Origin = Vector2.One / 2f;
		RotateComponent rotateComponent = testEntity.AddComponent<RotateComponent>();
	}

	public Camera Camera { get; }

	public Entity CreateEntity()
	{
		var entity = EntityFactory.NewEntity(this);
		entities.Add(entity);
		return entity;
	}

	public void Update(GameTime time)
	{
		foreach (Entity entity in entities)
			entity.Update(time);
	}
	
	public void Render(GameTime time)
	{
		if (Camera == null)
			return;
		
		using SpriteBatch spriteBatch = new SpriteBatch(GraphicsDevice);
		spriteBatch.Begin(SpriteSortMode.Deferred,
			BlendState.AlphaBlend,
			SamplerState.PointClamp,
			null,
			null,
			null,
			Camera.TransformMatrix
		);
		foreach (Entity entity in entities)
			entity.Render(spriteBatch);
		spriteBatch.End();
	}
}
