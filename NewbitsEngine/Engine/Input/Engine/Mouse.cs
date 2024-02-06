using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using NewbitsEngine.Engine.Input.Binding;
using NewbitsEngine.Engine.Input.Enum;

namespace NewbitsEngine.Engine.Input.Engine;

public class Mouse : InputEngine
{
    public Mouse(InputManager manager) : base(manager)
    {
    }

    private MouseState PreviousState { get; set; }
    private MouseState CurrentState { get; set; }

    public int CurrentScrollWheelValue => CurrentState.ScrollWheelValue;
    public int PreviousScrollWheelValue => PreviousState.ScrollWheelValue;
    public int ScrollWheelDelta => CurrentScrollWheelValue - PreviousScrollWheelValue;

    public Vector2 PreviousCursorPosition => new(PreviousState.X, PreviousState.Y);
    public Vector2 CurrentCursorPosition => new(CurrentState.X, CurrentState.Y);
    public Vector2 CursorPositionDelta => CurrentCursorPosition - PreviousCursorPosition;

    public bool Scrolled => ScrollWheelDelta != 0;
    public bool CursorMoved => CursorPositionDelta != Vector2.Zero;

    public override void Setup()
    {
        PreviousState = Microsoft.Xna.Framework.Input.Mouse.GetState();
        CurrentState = Microsoft.Xna.Framework.Input.Mouse.GetState();
    }

    public override void Update()
    {
        PreviousState = CurrentState;
        CurrentState = Microsoft.Xna.Framework.Input.Mouse.GetState();
    }

    public bool IsReleased(MouseButton button)
    {
        return GetButtonState(button, CurrentState) == ButtonState.Released;
    }

    public bool WasReleased(MouseButton button)
    {
        return GetButtonState(button, PreviousState) == ButtonState.Released;
    }

    public bool IsPressed(MouseButton button)
    {
        return GetButtonState(button, CurrentState) == ButtonState.Pressed;
    }

    public bool WasPressed(MouseButton button)
    {
        return GetButtonState(button, PreviousState) == ButtonState.Pressed;
    }

    public static void SetMouseCoordinates(int x, int y)
    {
        Microsoft.Xna.Framework.Input.Mouse.SetPosition(x, y);
    }

    private static ButtonState GetButtonState(MouseButton button, MouseState state)
    {
        return button switch
        {
            MouseButton.LeftButton => state.LeftButton,
            MouseButton.RightButton => state.RightButton,
            MouseButton.MiddleButton => state.MiddleButton,
            MouseButton.XButton1 => state.XButton1,
            MouseButton.XButton2 => state.XButton2,
            _ => throw new ArgumentOutOfRangeException(nameof(button), button, null)
        };
    }

    public MouseDeltaBinding MouseDelta()
    {
        return new MouseDeltaBinding(this);
    }
    
    public MousePositionBinding MousePosition()
    {
        return new MousePositionBinding(this);
    }
    
    public MouseWheelBinding WheelDelta()
    {
        return new MouseWheelBinding(this);
    }
}