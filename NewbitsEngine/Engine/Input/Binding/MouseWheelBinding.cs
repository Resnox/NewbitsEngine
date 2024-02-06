using NewbitsEngine.Engine.Input.Value;

using Mouse = NewbitsEngine.Engine.Input.Engine.Mouse;

namespace NewbitsEngine.Engine.Input.Binding;

public class MouseWheelBinding : IInputValue<float>
{
    private readonly Mouse engine;

    public MouseWheelBinding(Mouse engine)
    {
        this.engine = engine;
    }

    #region IInputValue<float> Members

    public float GetValue()
    {
        return engine.ScrollWheelDelta;
    }

    #endregion
}