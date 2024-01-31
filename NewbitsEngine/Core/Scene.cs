using System;
using System.Collections.Generic;

namespace NewbitsEngine.Core;

public sealed class Scene
{
    private readonly List<Entity> entities;

    public Scene()
    {
        entities = new List<Entity>();

        var cameraEntity = new Entity(this);
        Camera = cameraEntity.AddComponent<Camera>();
    }

    public Camera Camera { get; }

    public void Update(TimeSpan time)
    {
        foreach (var entity in entities)
            entity.Update(time);
    }
}