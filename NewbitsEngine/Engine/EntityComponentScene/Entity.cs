using System;
using System.Collections.Generic;

namespace NewbitsEngine.Core;

public sealed class Entity
{
    private static uint _lastId;

    private readonly Dictionary<Type, Component> _components;
    private bool _isDestroyed;

    public Scene Scene { get; private set; }

    public Entity(Scene scene)
    {
        Scene = scene;
        _isDestroyed = false;
        _components = new Dictionary<Type, Component>();
        Transform = new Transform(this);
        Id = _lastId++;
    }

    public uint Id { get; }

    public Transform Transform { get; }

    public void Destroy()
    {
        if (_isDestroyed)
            return;
        
        if (Scene == null)
            return;

        _isDestroyed = true;
        Transform.Parent = null;

        for (var i = Transform.ChildCount - 1; i >= 0; i--)
            Transform.GetChild(i).entity.Destroy();

        _components.Clear();
    }

    public T AddComponent<T>() where T : Component
    {
        var component = (T)Activator.CreateInstance(typeof(T), this);
        _components.Add(typeof(T), component);

        return component;
    }

    public T GetComponent<T>() where T : Component
    {
        return _components.TryGetValue(typeof(T), out var component) ? (T)component : null;
    }

    public bool RemoveComponent<T>() where T : Component
    {
        return _components.Remove(typeof(T));
    }

    public void Update(TimeSpan time)
    {
        if (_isDestroyed)
            return;
        
        foreach (var component in _components.Values)
            if (component is IUpdatable updatableComponent && component.Enabled)
                updatableComponent.Update(time);
    }
}