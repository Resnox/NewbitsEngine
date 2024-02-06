using Microsoft.Xna.Framework;

using NewbitsEngine.Engine.Input.Enum;
using NewbitsEngine.Engine.Input.Value;

using GamePad = NewbitsEngine.Engine.Input.Engine.GamePad;

namespace NewbitsEngine.Engine.Input.Binding;

public class GamePadButtonBinding : IInputValue<float>
{
    private readonly GamePadButton button;
    private readonly GamePad engine;
    private readonly PlayerIndex playerIndex;

    public GamePadButtonBinding(GamePad engine, GamePadButton button, PlayerIndex playerIndex)
    {
        this.engine = engine;
        this.button = button;
        this.playerIndex = playerIndex;
    }

    #region IInputValue<float> Members

    public float GetValue()
    {
        return engine.IsPressed(button, playerIndex) ? 1f : 0f;
    }

    #endregion
}