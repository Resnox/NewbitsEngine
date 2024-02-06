using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using NewbitsEngine.Engine.Input.Binding;
using NewbitsEngine.Engine.Input.Enum;

namespace NewbitsEngine.Engine.Input.Engine;

public class GamePad : InputEngine
{
    public GamePad(InputManager manager) : base(manager)
    {
        PreviousStates = new Dictionary<PlayerIndex, GamePadState>();
        CurrentStates = new Dictionary<PlayerIndex, GamePadState>();
    }

    private Dictionary<PlayerIndex, GamePadState> PreviousStates { get; }
    private Dictionary<PlayerIndex, GamePadState> CurrentStates { get; }

    public override void Setup()
    {
        foreach (var playerIndex in System.Enum.GetValues<PlayerIndex>())
        {
            PreviousStates[playerIndex] = Microsoft.Xna.Framework.Input.GamePad.GetState(playerIndex);
            CurrentStates[playerIndex] = Microsoft.Xna.Framework.Input.GamePad.GetState(playerIndex);
        }
    }

    public override void Update()
    {
        foreach (var playerIndex in System.Enum.GetValues<PlayerIndex>())
        {
            PreviousStates[playerIndex] = CurrentStates[playerIndex];
            CurrentStates[playerIndex] = Microsoft.Xna.Framework.Input.GamePad.GetState(playerIndex);
        }
    }

    public bool IsReleased(GamePadButton button, PlayerIndex playerIndex)
    {
        return GetButtonState(button, CurrentStates[playerIndex]) == ButtonState.Released;
    }

    public bool WasReleased(GamePadButton button, PlayerIndex playerIndex)
    {
        return GetButtonState(button, PreviousStates[playerIndex]) == ButtonState.Released;
    }

    public bool IsPressed(GamePadButton button, PlayerIndex playerIndex)
    {
        return GetButtonState(button, CurrentStates[playerIndex]) == ButtonState.Pressed;
    }

    public bool WasPressed(GamePadButton button, PlayerIndex playerIndex)
    {
        return GetButtonState(button, PreviousStates[playerIndex]) == ButtonState.Pressed;
    }

    public Vector2 GetLeftJoystick(PlayerIndex playerIndex)
    {
        return CurrentStates[playerIndex].ThumbSticks.Left;
    }

    public Vector2 GetRightJoystick(PlayerIndex playerIndex)
    {
        return CurrentStates[playerIndex].ThumbSticks.Right;
    }

    public float GetLeftTrigger(PlayerIndex playerIndex)
    {
        return CurrentStates[playerIndex].Triggers.Left;
    }

    public float GetRightTrigger(PlayerIndex playerIndex)
    {
        return CurrentStates[playerIndex].Triggers.Right;
    }

    private static ButtonState GetButtonState(GamePadButton button, GamePadState state)
    {
        return button switch
        {
            GamePadButton.X => state.Buttons.X,
            GamePadButton.Y => state.Buttons.Y,
            GamePadButton.A => state.Buttons.A,
            GamePadButton.B => state.Buttons.B,
            GamePadButton.Back => state.Buttons.X,
            GamePadButton.Start => state.Buttons.Start,
            GamePadButton.LeftShoulder => state.Buttons.LeftShoulder,
            GamePadButton.RightShoulder => state.Buttons.RightShoulder,
            GamePadButton.LeftStick => state.Buttons.LeftStick,
            GamePadButton.RightStick => state.Buttons.RightStick,
            GamePadButton.BigButton => state.Buttons.BigButton,
            GamePadButton.DPadDown => state.DPad.Down,
            GamePadButton.DPadLeft => state.DPad.Left,
            GamePadButton.DPadRight => state.DPad.Right,
            GamePadButton.DPadUp => state.DPad.Up,
            _ => throw new ArgumentOutOfRangeException(nameof(button), button, null)
        };
    }

    public GamePadButtonBinding Button(GamePadButton button, PlayerIndex playerIndex)
    {
        return new GamePadButtonBinding(this, button, playerIndex);
    }

    public GamePadTriggerBinding Trigger(GamePadTrigger trigger, PlayerIndex playerIndex)
    {
        return new GamePadTriggerBinding(this, trigger, playerIndex);
    }

    public GamePadJoystickBinding Joystick(GamePadJoystick joystick, PlayerIndex playerIndex)
    {
        return new GamePadJoystickBinding(this, joystick, playerIndex);
    }
}