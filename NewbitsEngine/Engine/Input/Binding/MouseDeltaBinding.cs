using Microsoft.Xna.Framework;

using NewbitsEngine.Engine.Input.Engine;
using NewbitsEngine.Engine.Input.Value;

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
