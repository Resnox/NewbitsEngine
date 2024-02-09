using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using NewbitsEngine.Engine.ECS;
using NewbitsEngine.Engine.Input;

using Ninject;

using XnaGame = Microsoft.Xna.Framework.Game;

namespace NewbitsEngine;

public class GameCore : XnaGame
{
	private GraphicsDeviceManager GraphicsDeviceManager { get; }
	private InputManager InputManager { get; }
	private Scene currentScene;
	private IKernel kernel;
	private Texture2D texture2D;

	public GameCore()
	{
		GraphicsDeviceManager = new GraphicsDeviceManager(this);
		GraphicsDeviceManager.PreferredBackBufferWidth = 1280;
		GraphicsDeviceManager.PreferredBackBufferHeight = 720;
		GraphicsDeviceManager.SynchronizeWithVerticalRetrace = true;
		GraphicsDeviceManager.IsFullScreen = false;

		Window.AllowUserResizing = true;

		Content.RootDirectory = "Content";

		InputManager = new InputManager();

		IsMouseVisible = false;
		IsFixedTimeStep = false;
	}

	protected override void Initialize()
	{
		base.Initialize();
		kernel = new StandardKernel();
		kernel.Bind<GraphicsDevice>().ToConstant(GraphicsDeviceManager.GraphicsDevice).InSingletonScope();
		kernel.Bind<Texture2D>().ToConstant(texture2D).InSingletonScope();
		kernel.Bind<InputManager>().ToConstant(InputManager).InSingletonScope();
		kernel.Bind<ContentManager>().ToConstant(Content).InSingletonScope();

		currentScene = kernel.Get<Scene>();
	}

	protected override void LoadContent()
	{
		base.LoadContent();
		texture2D = Content.Load<Texture2D>("Resnox");
	}

	protected override void UnloadContent()
	{
		base.UnloadContent();
	}

	protected override void Update(GameTime gameTime)
	{
		base.Update(gameTime);
		currentScene.Update(gameTime);
	}

	protected override void Draw(GameTime gameTime)
	{
		base.Draw(gameTime);
		GraphicsDevice.Clear(Color.CornflowerBlue);
		currentScene.Render(gameTime);
	}
}
