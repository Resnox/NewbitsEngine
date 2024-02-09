using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using NewbitsEngine.Engine.ECS.Components;

namespace NewbitsEngine.Engine.ECS;

public sealed class Entity
{
	private static uint lastId;

	private readonly Dictionary<Type, Component> components;
	private bool isDestroyed;

	public Entity(Scene scene, ComponentFactory componentFactory)
	{
		ComponentFactory = componentFactory;
		Scene = scene;
		isDestroyed = false;
		components = new Dictionary<Type, Component>();
		Transform = new Transform(this);
		Id = lastId++;
	}
	private ComponentFactory ComponentFactory { get; }
	public Scene Scene { get; }

	public uint Id { get; }

	public Transform Transform { get; }

	public void Destroy()
	{
		if (isDestroyed)
			return;

		if (Scene == null)
			return;

		isDestroyed = true;
		Transform.Parent = null;

		for (int i = Transform.ChildCount - 1; i >= 0; i--)
			Transform.GetChild(i).entity.Destroy();

		components.Clear();
	}

	public T AddComponent<T>() where T : Component
	{
		T component = ComponentFactory.NewComponent<T>(this);
		components.Add(typeof(T), component);

		return component;
	}

	public T GetComponent<T>() where T : Component
	{
		return components.TryGetValue(typeof(T), out Component component) ? (T) component : null;
	}

	public bool RemoveComponent<T>() where T : Component
	{
		return components.Remove(typeof(T));
	}

	public void Update(GameTime time)
	{
		if (isDestroyed)
			return;

		foreach (Component component in components.Values)
			if (component is IUpdatable updatableComponent && component.Enabled)
				updatableComponent.Update(time);
	}

	public void Render(SpriteBatch spriteBatch)
	{
		if (isDestroyed)
			return;

		foreach (Component component in components.Values)
			if (component is IRenderable renderableComponent && component.Enabled)
				renderableComponent.Render(spriteBatch);
	}
}
