using System;
using System.Data.SqlClient;

using Microsoft.Xna.Framework;

namespace NewbitsEngine.Core;

public sealed class Camera : Component, IUpdatable
{
    public const float MIN_ZOOM = 1f;
    public const float MAX_ZOOM = 20f;

    private GraphicsDeviceManager _graphicsDeviceManager;

    public Matrix TransformMatrix { get; private set; }
    public Matrix InverseTransformMatrix { get; private set; }
    

    private float _zoom;
    public float Zoom
    {
        get => _zoom;
        set => _zoom = MathHelper.Clamp(value, MIN_ZOOM, MAX_ZOOM);
    }
    
    public Camera(Entity entity, GraphicsDeviceManager graphicsDeviceManager) : base(entity)
    {
        _graphicsDeviceManager = graphicsDeviceManager;
        _zoom = 10f;
    }
    
    public void Update(TimeSpan time)
    {
        TransformMatrix = Matrix.CreateTranslation(-Transform.Position.X, -Transform.Position.Y, 0) * 
            Matrix.CreateScale(_zoom) * 
            Matrix.CreateTranslation(
                _graphicsDeviceManager.GraphicsDevice.PresentationParameters.BackBufferWidth / 2f, 
                _graphicsDeviceManager.GraphicsDevice.PresentationParameters.BackBufferWidth / 2f, 
                0f
            )
        ;
        InverseTransformMatrix = Matrix.Invert(TransformMatrix);
    }
}