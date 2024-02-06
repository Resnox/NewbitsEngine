using Microsoft.Xna.Framework;

using NewbitsEngine.Engine.Input.Value;

using Mouse = NewbitsEngine.Engine.Input.Engine.Mouse;

namespace NewbitsEngine.Engine.Input.Binding;

public class MouseDeltaBinding : IInputValue<Vector2>
{
    private readonly Mouse engine;

    public MouseDeltaBinding(Mouse engine)
    {
        this.engine = engine;
    }

    #region IInputValue<float> Members

    public Vector2 GetValue()
    {
        return engine.CursorPositionDelta;
    }

    #endregion
}