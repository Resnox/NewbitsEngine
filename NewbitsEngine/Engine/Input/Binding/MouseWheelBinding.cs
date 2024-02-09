using NewbitsEngine.Engine.Input.Engine;
using NewbitsEngine.Engine.Input.Value;

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
