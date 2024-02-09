using Microsoft.Xna.Framework.Input;

using NewbitsEngine.Engine.Input.Binding;

namespace NewbitsEngine.Engine.Input.Engine;

public class Keyboard : InputEngine
{
	public Keyboard(InputManager manager) : base(manager)
	{
	}

	private KeyboardState PreviousState { get; set; }
	private KeyboardState CurrentState { get; set; }

	public override void Setup()
	{
		PreviousState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
		CurrentState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
	}

	public override void Update()
	{
		PreviousState = CurrentState;
		CurrentState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
	}

	public bool IsKeyPressed(Keys key)
	{
		return CurrentState.IsKeyDown(key);
	}

	public bool WasKeyPressed(Keys key)
	{
		return PreviousState.IsKeyDown(key);
	}

	public bool IsKeyReleased(Keys key)
	{
		return CurrentState.IsKeyUp(key);
	}

	public bool WasKeyReleased(Keys key)
	{
		return PreviousState.IsKeyUp(key);
	}

	public KeyBinding KeyBinding(Keys key)
	{
		return new KeyBinding(this, key);
	}

	public KeyUpBinding KeyUpBinding(Keys key)
	{
		return new KeyUpBinding(this, key);
	}

	public KeyDownBinding KeyDownBinding(Keys key)
	{
		return new KeyDownBinding(this, key);
	}
}
