using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using NewbitsEngine.Engine.ECS;

using Ninject;

using XnaGame = Microsoft.Xna.Framework.Game;

namespace NewbitsEngine;

public class GameCore : XnaGame
{
	private IKernel kernel;
	private Scene currentScene;
	private Texture2D texture2D;
	private GraphicsDeviceManager graphicsDeviceManager;
	
	public GameCore()
	{
		graphicsDeviceManager = new GraphicsDeviceManager(this);
		graphicsDeviceManager.PreferredBackBufferWidth = 1280;
		graphicsDeviceManager.PreferredBackBufferHeight = 720;
		graphicsDeviceManager.SynchronizeWithVerticalRetrace = true;
		graphicsDeviceManager.IsFullScreen = false;

		Window.AllowUserResizing = true;

		Content.RootDirectory = "Content";

		IsMouseVisible = false;
		IsFixedTimeStep = false;
	}

	protected override void Initialize()
	{
		base.Initialize();
		kernel = new StandardKernel();
		kernel.Bind<GraphicsDevice>().ToConstant(graphicsDeviceManager.GraphicsDevice);
		kernel.Bind<Texture2D>().ToConstant(texture2D);
		
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
