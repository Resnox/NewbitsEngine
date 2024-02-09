using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NewbitsEngine.Engine.ECS.Components;

public sealed class Camera : Component, IUpdatable
{
	public const float MIN_ZOOM = 1f;
	public const float MAX_ZOOM = 20f;


	private float zoom;

	public Camera(Entity entity, GraphicsDevice graphicsDevice) : base(entity)
	{
		TargetScreenSize = new Point(1280, 720);
		GraphicsDevice = graphicsDevice;
		zoom = 1 / 5f;
	}
	public Point TargetScreenSize { get; set; }

	private GraphicsDevice GraphicsDevice { get; }

	public Matrix TransformMatrix { get; private set; }
	public Matrix InverseTransformMatrix { get; private set; }
	public float Zoom
	{
		get
		{
			return zoom;
		}
		set
		{
			zoom = MathHelper.Clamp(value, MIN_ZOOM, MAX_ZOOM);
		}
	}

	public void Update(GameTime time)
	{
		TransformMatrix = Matrix.CreateTranslation(-Transform.Position.X, -Transform.Position.Y, 0) *
			Matrix.CreateScale(zoom) *
			Matrix.CreateScale(MathHelper.Min(GraphicsDevice.PresentationParameters.BackBufferWidth / (float) TargetScreenSize.X, GraphicsDevice.PresentationParameters.BackBufferHeight / (float) TargetScreenSize.Y)) *
			Matrix.CreateTranslation(
				GraphicsDevice.PresentationParameters.BackBufferWidth / 2f,
				GraphicsDevice.PresentationParameters.BackBufferHeight / 2f,
				0f
			)
			;

		InverseTransformMatrix = Matrix.Invert(TransformMatrix);
	}
}
