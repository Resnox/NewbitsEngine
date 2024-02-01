using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XnaGame = Microsoft.Xna.Framework.Game;

namespace NewbitsEngine;

public class GameCore : XnaGame
{
    public GameCore()
    {
        Gdm = new GraphicsDeviceManager(this);
        Gdm.PreferredBackBufferWidth = 720;
        Gdm.PreferredBackBufferHeight = 405;
        Gdm.SynchronizeWithVerticalRetrace = true;
        Gdm.IsFullScreen = false;

        Window.AllowUserResizing = true;

        Content.RootDirectory = "Content";

        IsMouseVisible = false;
        IsFixedTimeStep = false;
    }

    public GraphicsDeviceManager Gdm { get; }
    public SpriteBatch SpriteBatch { get; private set; }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        base.LoadContent();

        SpriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void UnloadContent()
    {
        base.UnloadContent();

        SpriteBatch.Dispose();
    }

    protected override void OnExiting(object sender, EventArgs args)
    {
        base.OnExiting(sender, args);
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        GraphicsDevice.Clear(Color.CornflowerBlue);
        SpriteBatch.Begin(SpriteSortMode.Deferred,
            BlendState.AlphaBlend,
            SamplerState.PointClamp,
            null,
            null,
            null,
            Matrix.Identity //Camera.Transform
        );
        
        SpriteBatch.End();
    }
}