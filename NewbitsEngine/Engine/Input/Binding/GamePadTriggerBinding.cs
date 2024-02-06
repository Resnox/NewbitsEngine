using System;

using Microsoft.Xna.Framework;

using NewbitsEngine.Engine.Input.Enum;
using NewbitsEngine.Engine.Input.Value;

using GamePad = NewbitsEngine.Engine.Input.Engine.GamePad;

namespace NewbitsEngine.Engine.Input.Binding;

public class GamePadTriggerBinding : IInputValue<float>
{
    private readonly GamePad engine;
    private readonly PlayerIndex playerIndex;
    private readonly GamePadTrigger trigger;

    public GamePadTriggerBinding(GamePad engine, GamePadTrigger trigger, PlayerIndex playerIndex)
    {
        this.engine = engine;
        this.trigger = trigger;
        this.playerIndex = playerIndex;
    }

    #region IInputValue<float> Members

    public float GetValue()
    {
        return trigger switch
        {
            GamePadTrigger.LeftTrigger => engine.GetLeftTrigger(playerIndex),
            GamePadTrigger.RightTrigger => engine.GetRightTrigger(playerIndex),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    #endregion
}