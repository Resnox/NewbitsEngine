using System;

using Microsoft.Xna.Framework;

using NewbitsEngine.Engine.Input.Engine;
using NewbitsEngine.Engine.Input.Enum;
using NewbitsEngine.Engine.Input.Value;

namespace NewbitsEngine.Engine.Input.Binding;

public class GamePadJoystickBinding : IInputValue<Vector2>
{
	private readonly GamePad engine;
	private readonly GamePadJoystick joystick;
	private readonly PlayerIndex playerIndex;

	public GamePadJoystickBinding(GamePad engine, GamePadJoystick joystick, PlayerIndex playerIndex)
	{
		this.engine = engine;
		this.joystick = joystick;
		this.playerIndex = playerIndex;
	}

	#region IInputValue<Vector2> Members

	public Vector2 GetValue()
	{
		return joystick switch
		{
			GamePadJoystick.LeftStick => engine.GetLeftJoystick(playerIndex),
			GamePadJoystick.RightStick => engine.GetRightJoystick(playerIndex),
			_ => throw new ArgumentOutOfRangeException()
		};
	}

	#endregion
}
